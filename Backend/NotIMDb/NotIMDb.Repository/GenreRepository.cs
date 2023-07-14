using NotIMDb.Common;
using NotIMDb.Model;
using NotIMDb.Repository.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Repository
{
    public class GenreRepository : IGenreRepository
    {

        NpgsqlConnection connection = new NpgsqlConnection(ConnectionStringHelper.Get());
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                using (connection)
                {
                    await connection.OpenAsync();
                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    command.CommandText = ($"UPDATE \"Genre\" set \"IsActive\"=false WHERE \"Id\"=@id;");
                    command.Parameters.AddWithValue("@id", id);
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<PagedList<Genre>> GetAsync(Paging paging, Sorting sorting, GenreFiltering filtering)
        {
            List<Genre> genres = new List<Genre>();
            try
            {
                using (connection)
                {
                    await connection.OpenAsync();
                    NpgsqlCommand command = new NpgsqlCommand();
                    StringBuilder queryBuilder = new StringBuilder();
                    queryBuilder.Append("SELECT * FROM \"Genre\"");
                    if (filtering != null)
                    {
                        queryBuilder.Append(" WHERE 1=1");
                        if (filtering.FilterString != null)
                        {
                            queryBuilder.Append(" or \"Title\" LIKE @filterString%");
                            command.Parameters.AddWithValue("@filterString", filtering.FilterString);
                        }
                    }
                    if (sorting.Orderby != null)
                    {
                        queryBuilder.Append($" ORDER BY \"{sorting.Orderby}\" {sorting.SortOrder}");
                        if (paging != null)
                        {
                            queryBuilder.Append(" OFFSET @offset LIMIT @pageSize");
                            command.Parameters.AddWithValue("@offset", (paging.CurrentPage - 1) * paging.PageSize);
                            command.Parameters.AddWithValue("@pageSize", paging.PageSize);
                        }
                    }
                    queryBuilder.Append(";");
                    command.Connection = connection;
                    command.CommandText = queryBuilder.ToString();
                    using (command)
                    {
                        NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                Genre genre = new Genre();
                                genre.Id = (Guid)reader["Id"];
                                genre.Title = (string)reader["Title"];                                
                                genre.IsActive = reader["IsActive"] != DBNull.Value && (bool)reader["IsActive"];
                                genre.CreatedByUserId = reader["CreatedByUserId"] != DBNull.Value ? (Guid)reader["CreatedByUserId"] : Guid.Empty;
                                genre.UpdatedByUserId = reader["UpdatedByUserId"] != DBNull.Value ? (Guid)reader["UpdatedByUserId"] : Guid.Empty;
                                genre.DateCreated = (DateTime)reader["DateCreated"];
                                genre.DateUpdated = reader["DateUpdated"] != DBNull.Value ? (DateTime)reader["DateUpdated"] : DateTime.MinValue;
                                genres.Add(genre);
                            }
                        }
                    }
                    await connection.CloseAsync();
                    return new PagedList<Genre>(genres, genres.Count, paging.CurrentPage, paging.PageSize);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Task<Genre> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> PostAsync(Guid guid, DateTime time, bool isActive, Genre genre)
        {
            genre.CreatedByUserId = Guid.Parse("e2a804b7-964e-4f8a-a125-2eb492cc3108");
            genre.UpdatedByUserId = Guid.Parse("e2a804b7-964e-4f8a-a125-2eb492cc3108");
            try
            {
                await connection.OpenAsync();
                using (connection)
                {
                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    command.CommandText = ($"INSERT INTO \"Genre\" (\"Id\", \"Title\", \"IsActive\", \"CreatedByUserId\", \"UpdatedByUserId\", \"DateCreated\", \"DateUpdated\") VALUES (@Id, @Title, @IsActive, @CreatedByUserId, @UpdatedByUserId, @DateCreated, @DateUpdated)");
                    command.Parameters.AddWithValue("@Id", guid);
                    command.Parameters.AddWithValue("@Title", genre.Title);
                    command.Parameters.AddWithValue("@IsActive", isActive);
                    command.Parameters.AddWithValue("@CreatedByUserId", genre.CreatedByUserId);
                    command.Parameters.AddWithValue("@UpdatedByUserId", genre.UpdatedByUserId);
                    command.Parameters.AddWithValue("@DateCreated", time);
                    command.Parameters.AddWithValue("@DateUpdated", time);
                    await command.ExecuteNonQueryAsync();
                    await connection.CloseAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> PutAsync(Guid id, Genre genre, DateTime time, Guid adminId)
        {
            try
            {
                using (connection)
                {
                    await connection.OpenAsync();
                    StringBuilder stringBuilder = new StringBuilder();
                    NpgsqlCommand command = new NpgsqlCommand();
                    stringBuilder.Append("UPDATE \"Genre\" set ");
                    if (genre.Title != null)
                    {
                        stringBuilder.Append("\"Title\"=@title,");
                        command.Parameters.AddWithValue("@title", genre.Title);
                    }
                    stringBuilder.Append(" \"UpdatedByUserId\"=@updatedByUserId,");
                    command.Parameters.AddWithValue("@updatedByUserId", adminId);
                    stringBuilder.Append(" \"DateUpdated\"=@dateUpdated");
                    command.Parameters.AddWithValue("@dateUpdated", time);
                    stringBuilder.Append(" WHERE \"Id\"=@id;");
                    command.Parameters.AddWithValue("@id", id);
                    command.Connection = connection;
                    command.CommandText = stringBuilder.ToString();

                    if (await command.ExecuteNonQueryAsync() != -1)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
