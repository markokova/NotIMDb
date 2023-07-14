using NotIMDb.Common;
using NotIMDb.Model;
using NotIMDb.Repository.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private string connectionString = ConnectionStringHelper.Get();
        public async Task<PagedList<Review>> GetReviewsAsync(Sorting sorting, Paging paging, ReviewFiltering filtering)
        {
            List<Review> queriedReviews = new List<Review>();
            int resultsTotalNumber = 0;

            string startingQuery = "FROM \"Review\" r " +
              "INNER JOIN \"User\" u ON r.\"CreatedByUserId\" = u.\"Id\"";
            StringBuilder queryBuilder = new StringBuilder("SELECT r.\"Id\", r.\"Title\", r.\"Content\", r.\"DateCreated\", r.\"DateUpdated\", r.\"Score\", r.\"MovieId\", u.\"Id\" AS \"UserId\", u.\"FirstName\", u.\"LastName\" " + startingQuery);
            StringBuilder countResultsBuilder = new StringBuilder("SELECT COUNT(*) " + startingQuery);

            try
            {
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

                        command.Parameters.AddWithValue("@OffsetValue", ((paging.CurrentPage - 1) * (paging.PageSize - 1)));
                        command.Parameters.AddWithValue("@LimitValue", paging.PageSize);
                        command.Parameters.AddWithValue("@ScoreValue", filtering.Score ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@MovieId", filtering.MovieId);
                        command.Parameters.AddWithValue("@FilterString", filtering.FilterString ?? (object)DBNull.Value);
                        NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                Review review = new Review(); User user = new User();  Movie movie = new Movie();
                                user.Id = (Guid)reader["UserId"];
                                user.FirstName = (string)reader["FirstName"];
                                user.LastName = (string)reader["LastName"];
                                review.Id = (Guid)reader["Id"];
                                review.CreatedByUserId = user.Id;
                                review.UpdatedByUserId = user.Id;
                                review.DateCreated = (DateTime)reader["DateCreated"];
                                review.DateUpdated = (DateTime)reader["DateUpdated"];
                                review.Title = (string)reader["Title"];
                                review.Content = (string)reader["Content"];
                                review.Score = (int)reader["Score"];
                                review.MovieId = (Guid)reader["MovieId"];
                                review.User = user;
                                review.Movie = movie;
                                queriedReviews.Add(review);
                            }
                        }
                        reader.Close();
                    }
                    countResultsBuilder = FilterResults(countResultsBuilder, filtering);

                    using (NpgsqlCommand command = new NpgsqlCommand(countResultsBuilder.ToString(), connection))
                    {
                        command.Parameters.AddWithValue("@ScoreValue", filtering.Score ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@FilterString", filtering.FilterString ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@MovieId", filtering.MovieId);

                        object countResult = await command.ExecuteScalarAsync();
                        resultsTotalNumber = Convert.ToInt32(countResult);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            PagedList<Review> reviews = new PagedList<Review>(queriedReviews, resultsTotalNumber, paging.CurrentPage, paging.PageSize);
            return reviews;
        }

        public async Task<int> SaveNewReviewAsync(Review review)
        {
            int affectedRows = 0;
            string query = "INSERT INTO \"Review\" (\"Id\", \"Title\", \"Content\", \"Score\", \"MovieId\", \"IsActive\"," +
                " \"CreatedByUserId\", \"UpdatedByUserId\", \"DateCreated\", \"DateUpdated\") VALUES(@Id, @Title, @Content, @Score, @MovieId, " +
                "@IsActive, @CreatedByUserId, @UpdatedByUserId, @DateCreated, @DateUpdated)";
            try
            {
                using(NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using(NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id",review.Id);
                        command.Parameters.AddWithValue("@Title", review.Title);
                        command.Parameters.AddWithValue("@Content", review.Content);
                        command.Parameters.AddWithValue("@Score", review.Score);
                        command.Parameters.AddWithValue("@MovieId", review.MovieId);
                        command.Parameters.AddWithValue("@IsActive", review.IsActive);
                        command.Parameters.AddWithValue("@CreatedByUserId", review.CreatedByUserId);
                        command.Parameters.AddWithValue("@UpdatedByUserId", review.UpdatedByUserId);
                        command.Parameters.AddWithValue("@DateCreated", review.DateCreated);
                        command.Parameters.AddWithValue("@DateUpdated", review.DateUpdated);
                        affectedRows = await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return affectedRows;
        }

        public async Task<int> UpdateReviewAsync(Guid id, Review newReview, CurrentUser currentUser)
        {
            int affectedRows = 0;
             
            StringBuilder queryBuilder = new StringBuilder("UPDATE \"Review\" SET ");

            try
            {
                using(NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using(NpgsqlCommand command = new NpgsqlCommand())
                    {
                        if (!string.IsNullOrEmpty(newReview.Title))
                        {
                            queryBuilder.Append("\"Title\" = @Title,");
                            command.Parameters.AddWithValue("@Title", newReview.Title);
                        }
                        if (!string.IsNullOrEmpty(newReview.Content))
                        {
                            queryBuilder.Append("\"Content\" = @Content,");
                            command.Parameters.AddWithValue("@Content", newReview.Content);
                        }
                        
                        if (newReview.Score != 0)
                        {
                            queryBuilder.Append("\"Score\" = @Score,");
                            command.Parameters.AddWithValue("@Score", newReview.Score);
                        }

                        queryBuilder.Append("\"DateUpdated\" = @DateUpdated");
                        command.Parameters.AddWithValue("@DateUpdated", DateTime.Now);

                        queryBuilder.Append(" WHERE \"Review\".\"Id\" = @Id");
                        command.Parameters.AddWithValue("@Id", id);

                        queryBuilder.Append(" AND \"Review\".\"CreatedByUserId\" = @CurrentUser");
                        command.Parameters.AddWithValue("@CurrentUser", currentUser.Id);

                        command.CommandText = queryBuilder.ToString();
                        command.Connection = connection;
                        affectedRows = await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return affectedRows;
        }

        public async Task<int> DeleteReviewAsync(Guid id)
        {
            int affectedRows = 0;
            string query = "UPDATE \"Review\" SET \"IsActive\" = false WHERE \"Id\" = @Id";

            using(NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using(NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id",id);

                    affectedRows = await command.ExecuteNonQueryAsync();
                }
            }
            return affectedRows;
        }

        //private async Task<Review> GetReviewByIdAsync(Guid id)
        //{
        //    Review review = null;
        //    string query = "SELECT FROM \"Review\" WHERE \"Id\" = @Id";
        //    try
        //    {
        //        using(NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        //        {
        //            connection.Open();
        //            using(NpgsqlCommand command = new NpgsqlCommand(query, connection))
        //            {
        //                command.Parameters.AddWithValue("@Id", id);

        //                NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        //                if (reader.HasRows)
        //                {
        //                    review = new Review();
        //                    await reader.ReadAsync();
        //                    //User user = new User();  Movie movie = new Movie();
        //                    //user.Id = (Guid)reader["UserId"];
        //                    //user.FirstName = (string)reader["FirstName"];
        //                    //user.LastName = (string)reader["LastName"];
        //                    //movie.Id = (Guid)reader["MovieId"];
        //                    //movie.Title = (string)reader["Title"];
        //                    //movie.Runtime = (int)reader["Runtime"];
        //                    //movie.YearOfRelease = (DateTime)reader["YearOfRelease"];
        //                    review.Id = (Guid)reader["Id"];
        //                    review.CreatedByUserId = (Guid)reader["UserId"];
        //                    review.UpdatedByUserId = (Guid)reader["UserId"];
        //                    review.MovieId = (Guid)reader["MovieId"];
        //                    review.Title = (string)reader["Title"];
        //                    review.Content = (string)reader["Content"];
        //                    review.Score = (int)reader["Score"];
        //                    //review.User = user;
        //                    //review.Movie = movie;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return review;
        //}adorio poslovi

        private StringBuilder FilterResults(StringBuilder builder, ReviewFiltering filtering)
        {
            builder.Append(" WHERE 1 = 1");

            if (filtering != null)
            {
                if (filtering.Score.HasValue)
                {
                    builder.Append(" AND r.\"Score\" = @ScoreValue");
                }
                if (!string.IsNullOrEmpty(filtering.FilterString))
                {
                    builder.Append(" AND (r.\"Title\" LIKE '%' || @FilterString || '%' OR r.\"Content\" LIKE '%' || @FilterString || '%')");
                }
                if(filtering.MovieId.HasValue && filtering.MovieId != Guid.Empty)
                {
                    builder.Append(" AND r.\"MovieId\" = @MovieId");
                }
            }

            return builder;
        }

        private StringBuilder SortResults(StringBuilder builder, Sorting sorting)
        {
            builder.Append($" ORDER BY r.\"{sorting.Orderby}\" ");
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
