using NotIMDb.Model;
using NotIMDb.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using NotIMDb.Common;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Numerics;

namespace NotIMDb.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private string connectionString = ConnectionStringHelper.Get();

        public async Task<PagedList<MovieView>> GetMoviesAsync(Sorting sorting, Paging paging, MovieFiltering filtering)
        {
            List<MovieView> movieViews = new List<MovieView>();

            string startingQuery = string.Empty;

            string joinPart = "LEFT JOIN \"MovieGenre\" mg ON mg.\"MovieId\" = m.\"Id\" " +
                "LEFT JOIN \"Genre\" g ON g.\"Id\" = mg.\"GenreId\" " +
                "LEFT JOIN \"ActorMovie\" am ON am.\"MovieId\" = m.\"Id\" " +
                "LEFT JOIN \"Actor\" a ON a.\"Id\" = am.\"ActorId\" " +
                "LEFT JOIN \"Review\" r ON r.\"MovieId\" = m.\"Id\" " +
                "LEFT JOIN \"WatchList\" wl ON wl.\"MovieId\" = m.\"Id\" ";

            StringBuilder queryBuilder = new StringBuilder("SELECT DISTINCT m.\"Id\" AS \"MovieId\", m.\"Title\" AS \"MovieTitle\", m.\"Runtime\", m.\"YearOfRelease\", m.\"Image\", " +
                "AVG(r.\"Score\") AS \"AverageScore\", " +
                "COALESCE((SELECT STRING_AGG(g1.\"Title\", ',') FROM \"Genre\" g1 INNER JOIN \"MovieGenre\" mg1 ON mg1.\"GenreId\" = g1.\"Id\" WHERE mg1.\"MovieId\" = m.\"Id\"), '') AS \"GenreTitle\", " +
                "COALESCE(STRING_AGG(DISTINCT CONCAT(a.\"FirstName\", ' ', a.\"LastName\"), ', '), '') AS \"ActorNames\" " +
                "FROM \"Movie\" m " +
                joinPart);

            StringBuilder queryCountBuilder = new StringBuilder("SELECT COUNT(*) FROM \"Movie\" m " + joinPart);

            int resultsTotalNumber = 0;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand())
                {

                    queryBuilder = FilterResults(queryBuilder, filtering);
                    queryBuilder = SortResults(queryBuilder, sorting);
                    queryBuilder = PageResults(queryBuilder);

                    command.Connection = connection;
                    command.CommandText = queryBuilder.ToString();

                    command.Parameters.AddWithValue("@UserId", filtering.UserId);
                    command.Parameters.AddWithValue("@GenreId", filtering.GenreId);
                    command.Parameters.AddWithValue("@OffsetValue", ((paging.CurrentPage - 1) * (paging.PageSize - 1)));
                    command.Parameters.AddWithValue("@LimitValue", paging.PageSize);
                   

                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            MovieView movieView = new MovieView(); Movie movie = new Movie();
                            movie.Id = (Guid)reader["MovieId"];
                            movie.Title = (string)reader["MovieTitle"];
                            movie.Runtime = (int)reader["Runtime"];
                            movie.Image = reader["Image"] != DBNull.Value ? (string)reader["Image"] : "";
                            movie.YearOfRelease = (DateTime)reader["YearOfRelease"];
                            movieView.Movie = movie;
                            movieView.AverageScore = reader["AverageScore"] != DBNull.Value ? (decimal)reader["AverageScore"] : 3;

                            movieView.Genres = (string)reader["GenreTitle"];
                            movieView.Actors = (string)reader["ActorNames"];
                            //movieView.Genres = reader["GenreTitle"] != DBNull.Value ? (string)reader["GenreTitle"] : "";
                            //movieView.Actors = reader["ActorNames"] != DBNull.Value ? (string)reader["ActorNames"] : "";
                            
                            movieViews.Add(movieView);
                        }
                    }
                    reader.Close();
                }

                //queryCountBuilder.Append("LEFT JOIN \"WatchList\" wl ON wl.\"MovieId\" = m.\"Id\"");
                queryCountBuilder = FilterResults(queryCountBuilder, filtering);

                using (NpgsqlCommand command = new NpgsqlCommand(queryCountBuilder.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@UserId", filtering.UserId);
                    command.Parameters.AddWithValue("@GenreId", filtering.GenreId);
                    command.Parameters.AddWithValue("@IsWatched", filtering.IsWatched);
                    object countResult = await command.ExecuteScalarAsync();
                    resultsTotalNumber = Convert.ToInt32(countResult);

                }
            }

            PagedList<MovieView> moviesPaged = new PagedList<MovieView>(movieViews, resultsTotalNumber, paging.CurrentPage, paging.PageSize);

            return moviesPaged;
        }

        public async Task<bool> AddMovieAsync(Movie newMovie)
        {
            if(newMovie == null)
            {
                return false;
            }

            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            try
            {
                using(connection)
                {
                    connection.Open();
                    NpgsqlTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        using(NpgsqlCommand command = new NpgsqlCommand())
                        {
                            command.CommandText = "INSERT INTO \"Movie\" (\"Id\", \"Title\", \"Runtime\", \"Image\", \"YearOfRelease\", \"IsActive\", \"CreatedByUserId\", \"UpdatedByUserId\", \"DateCreated\", \"DateUpdated\")" +
                            " VALUES (@id, @title, @runtime, @image, @yearOfRelease, @isActive, @createdByUserId, @updatedByUserId, @dateCreated, @dateUpdated)";
                            command.Connection = connection;

                            command.Parameters.AddWithValue("Id", newMovie.Id);
                            command.Parameters.AddWithValue("Title", newMovie.Title);
                            command.Parameters.AddWithValue("Runtime", newMovie.Runtime);
                            command.Parameters.AddWithValue("Image", newMovie.Image);
                            command.Parameters.AddWithValue("YearOfRelease", newMovie.YearOfRelease);
                            command.Parameters.AddWithValue("IsActive", newMovie.IsActive);
                            command.Parameters.AddWithValue("CreatedByUserId", newMovie.CreatedByUserId);
                            command.Parameters.AddWithValue("UpdatedByUserId", newMovie.UpdatedByUserId);
                            command.Parameters.AddWithValue("DateCreated", newMovie.DateCreated);
                            command.Parameters.AddWithValue("DateUpdated", newMovie.DateUpdated);

                            await command.ExecuteNonQueryAsync();
                        }

                        //insert into MovieGenre table
                        if(newMovie.GenreIds.Count > 0)
                        {
                            foreach (Guid genreId in newMovie.GenreIds)
                            {
                                using (NpgsqlCommand command = new NpgsqlCommand())
                                {
                                    Guid newGuid = Guid.NewGuid();
                                    command.CommandText = "INSERT INTO \"MovieGenre\" (\"Id\",\"MovieId\",\"GenreId\") VALUES (@Id, @Movieid, @GenreId)";
                                    command.Connection = connection;
                                    command.Parameters.AddWithValue("@MovieId", newMovie.Id);
                                    command.Parameters.AddWithValue("@GenreId", genreId);
                                    command.Parameters.AddWithValue("@Id", newGuid);

                                    await command.ExecuteNonQueryAsync();

                                }
                            }
                        }
                        
                        //insert into ActorMovie table
                        if(newMovie.ActorIds.Count > 0)
                        {
                            foreach (Guid actorId in newMovie.ActorIds)
                            {
                                using (NpgsqlCommand command = new NpgsqlCommand())
                                {
                                    Guid newGuid = Guid.NewGuid();
                                    command.CommandText = "INSERT INTO \"ActorMovie\" (\"Id\",\"MovieId\",\"ActorId\") VALUES (@Id, @Movieid, @ActorId)";
                                    command.Connection = connection;
                                    command.Parameters.AddWithValue("@MovieId", newMovie.Id);
                                    command.Parameters.AddWithValue("@ActorId", actorId);
                                    command.Parameters.AddWithValue("@Id", newGuid);

                                    await command.ExecuteNonQueryAsync();
                                }
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        transaction.Rollback();
                        
                        return false;
                        throw (ex);
                    }

                    transaction.Commit();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateMovieAsync(Guid id, Movie updateMovie)
        {            
            if(id == null)
            {
                return false;
            }

            StringBuilder queryBuilder = new StringBuilder("UPDATE \"Movie\" SET");

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using(NpgsqlCommand command = new NpgsqlCommand())
                    {
                        if(!string.IsNullOrEmpty(updateMovie.Title))
                        {
                            queryBuilder.Append(" \"Title\" = @title,");
                            command.Parameters.AddWithValue("@title", updateMovie.Title);
                        }
                        int runtime;
                        if (int.TryParse(updateMovie.Runtime.ToString(), out runtime))
                        {
                            queryBuilder.Append(" \"Runtime\" = @runtime,");
                            command.Parameters.AddWithValue("@runtime", updateMovie.Runtime);
                        }
                        DateTime yearOfRelease;
                        if (DateTime.TryParse(updateMovie.YearOfRelease.ToString(), out yearOfRelease))
                        {
                            queryBuilder.Append(" \"YearOfRelease\" = @yearOfRelease,");
                            command.Parameters.AddWithValue("@yearOfRelease", updateMovie.YearOfRelease);
                        }
                        if(!string.IsNullOrEmpty(updateMovie.Image))
                        {
                            queryBuilder.Append(" \"Image\" = @image,");
                            command.Parameters.AddWithValue("@image", updateMovie.Image);
                        }

                        queryBuilder.Append(" \"DateUpdated\" = @dateUpdated,");
                        command.Parameters.AddWithValue("@dateUpdated", updateMovie.DateUpdated);

                        queryBuilder.Append(" \"UpdatedByUserId\" = @updatedByUserId");
                        command.Parameters.AddWithValue("updatedByUserId", updateMovie.UpdatedByUserId);
                        
                        queryBuilder.Append(" WHERE \"Id\" = @Id");
                        command.Parameters.AddWithValue("@Id", id);

                        command.CommandText = queryBuilder.ToString();
                        command.Connection = connection;
                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }            
        }

        //TODO: add where id = userId for correct delete

        public async Task<bool> DeleteMovieAsync(Guid id)
        {
            if(id == null)
            {
                return false;
            }

            StringBuilder queryBuilder = new StringBuilder("UPDATE \"Movie\" SET \"IsActive\" = FALSE, ");

            try
            {
                using(NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        queryBuilder.Append("\"DateUpdated\" = @dateUpdated");
                        command.Parameters.AddWithValue("@dateUpdated", DateTime.UtcNow);

                        queryBuilder.Append(" WHERE \"Id\" = @Id");
                        command.Parameters.AddWithValue("@Id", id);

                        command.CommandText = queryBuilder.ToString();
                        command.Connection = conn;
                        await command.ExecuteNonQueryAsync();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //
        // GetMovieById method is currently a work in progress and is here for potential future usage
        // It will eventually get deleted if there's no use for it
        //
        public async Task<MovieView> GetMovieByIdAsync(Guid id)
        {
            MovieView movieView = null;
            string myQuery = "SELECT * FROM \"Movie\" WHERE \"Id\" = @Id";

            try
            {
                using(NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(myQuery, conn))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                        if(reader.HasRows)
                        {
                            movieView = new MovieView(); movieView.Movie = new Movie();
                            await reader.ReadAsync();

                            // User user = new User();
                            movieView.Movie.Id = (Guid)reader["Id"];
                            movieView.Movie.Title = (string)reader["Title"];
                            movieView.Movie.Runtime = (int)reader["Runtime"];
                            movieView.Movie.IsActive = (bool)reader["IsActive"];
                            movieView.Movie.Image = reader["Image"] != DBNull.Value ? (string)reader["Image"] : "";
                            //movie.AverageScore = (decimal)reader["AverageScore"];
                            movieView.Movie.YearOfRelease = (DateTime)reader["YearOfRelease"];
                            //movie.CreatedByUserId = (Guid)reader["CreatedByUserId"];
                            //movie.UpdatedByUserId = (Guid)reader["UpdatedByUserId"];
                            //movie.DateCreated = (DateTime)reader["DateCreated"];

                            movieView.Movie.DateUpdated = reader["DateUpdated"] != DBNull.Value ? (DateTime)reader["DateUpdated"] : DateTime.MinValue;

                            //movieView.Movie.DateUpdated = (DateTime)reader["DateUpdated"];
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return movieView;
        }

        private StringBuilder FilterResults(StringBuilder builder, MovieFiltering filtering)
        {
            builder.Append(" WHERE 1 = 1");

            if (filtering != null)
            {
                if (filtering.Runtime.HasValue)
                {
                    builder.Append(" AND m.\"Runtime\" = @RuntimeValue");

                }
                if(filtering.UserId.HasValue && filtering.UserId != Guid.Empty)
                {
                    builder.Append(" AND wl.\"UserId\" = @UserId");
                }
                if(filtering.GenreId.HasValue && filtering.GenreId != Guid.Empty)
                {
                    builder.Append(" AND mg.\"GenreId\" = @GenreId");
                }
            }
            builder.Append(" AND m.\"IsActive\" = true");
            if ((bool)filtering.ShouldFilterById)
            {
                builder.Append(" AND wl.\"IsActive\" = true");
                if ((bool)filtering.IsWatched)
                {
                    builder.Append(" AND wl.\"IsWatched\" = true");
                }
                else
                {
                    builder.Append(" AND wl.\"IsWatched\" = false");
                }
            }

            return builder;
        }

        private StringBuilder SortResults(StringBuilder builder, Sorting sorting)
        {
            builder.Append(" GROUP BY m.\"Id\", m.\"Title\", m.\"Runtime\", m.\"YearOfRelease\"");

            builder.Append($" ORDER BY m.\"{sorting.Orderby}\" ");
            if (sorting.SortOrder == "DESC")
            {
                builder.Append("DESC");
            }
            else
            {
                builder.Append("ASC");
            }
            return builder;
        }

        private StringBuilder PageResults(StringBuilder builder)
        {
            builder.Append(" OFFSET @OffsetValue");
            builder.Append(" LIMIT @LimitValue");
            return builder;
        }
    }
}
