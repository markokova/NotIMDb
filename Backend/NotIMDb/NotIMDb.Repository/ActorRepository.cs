using NotIMDb.Common;
using NotIMDb.Model;
using NotIMDb.Repository.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Repository
{
    public class ActorRepository : IActorRepository
    {

        NpgsqlConnection connection = new NpgsqlConnection(ConnectionStringHelper.Get());
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                NpgsqlConnection connection = new NpgsqlConnection(ConnectionStringHelper.Get());
                using (connection)
                {
                    await connection.OpenAsync();
                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    command.CommandText = ($"UPDATE \"Actor\" set \"IsActive\"=false WHERE \"Id\"=@id;");
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

        public async Task<PagedList<Actor>> GetAsync(Paging paging, Sorting sorting, ActorFiltering filtering)
        {
            List<Actor> actors = new List<Actor>();
            try
            {
                using (connection)
                {
                    await connection.OpenAsync();
                    NpgsqlCommand command = new NpgsqlCommand();
                    StringBuilder queryBuilder = new StringBuilder();
                    queryBuilder.Append("SELECT * FROM \"Actor\"");
                    if (filtering != null)
                    {
                        queryBuilder.Append(" WHERE 1=1");
                        if (filtering.FilterString != null)
                        {
                            queryBuilder.Append(" or \"FirstName\" LIKE @filterString%");
                            command.Parameters.AddWithValue("@filterString", filtering.FilterString);
                            queryBuilder.Append(" or \"LastName\" LIKE @filterString%");
                            command.Parameters.AddWithValue("@filterString", filtering.FilterString);
                            queryBuilder.Append(" or \"Bio\" LIKE @filterString%");
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
                                Actor actor = new Actor();
                                actor.Id = (Guid)reader["Id"];
                                actor.FirstName = (string)reader["FirstName"];
                                actor.LastName = (string)reader["LastName"];
                                actor.Bio = (string)reader["Bio"];
                                actor.Image = Convert.ToString(reader["Image"]);
                                actor.IsActive = reader["IsActive"] != DBNull.Value && (bool)reader["IsActive"];
                                actor.CreatedByUserId = reader["CreatedByUserId"] != DBNull.Value ? (Guid)reader["CreatedByUserId"] : Guid.Empty;
                                actor.UpdatedByUserId = reader["UpdatedByUserId"] != DBNull.Value ? (Guid)reader["UpdatedByUserId"] : Guid.Empty;
                                actor.DateCreated = (DateTime)reader["DateCreated"];
                                actor.DateUpdated = reader["DateUpdated"] != DBNull.Value ? (DateTime)reader["DateUpdated"] : DateTime.MinValue;
                                actors.Add(actor);
                            }
                        }
                    }
                    await connection.CloseAsync();
                    return new PagedList<Actor>(actors, actors.Count, paging.CurrentPage, paging.PageSize);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Actor> GetAsync(Guid id)
        {
            Actor actor = new Actor();
            try
            {
                using (connection)
                {
                    await connection.OpenAsync();
                    string query = "SELECT * FROM \"Actor\" WHERE \"Id\"=@Id";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                        if (reader.HasRows)
                        {
                            await reader.ReadAsync();
                        }
                        else
                        {
                            return actor;
                        }
                        actor.Id = (Guid)reader["Id"];
                        actor.FirstName = (string)reader["FirstName"];
                        actor.LastName = (string)reader["LastName"];
                        actor.Bio = (string)reader["Bio"];
                        actor.Image = Convert.ToString(reader["Image"]);
                        actor.IsActive = reader["IsActive"] != DBNull.Value && (bool)reader["IsActive"];
                        actor.CreatedByUserId = reader["CreatedByUserId"] != DBNull.Value ? (Guid)reader["CreatedByUserId"] : Guid.Empty;
                        actor.UpdatedByUserId = reader["UpdatedByUserId"] != DBNull.Value ? (Guid)reader["UpdatedByUserId"] : Guid.Empty;
                        actor.DateCreated = (DateTime)reader["DateCreated"];
                        actor.DateUpdated = reader["DateUpdated"] != DBNull.Value ? (DateTime)reader["DateUpdated"] : DateTime.MinValue; ;
                    }
                    await connection.CloseAsync();
                    return actor;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> PostAsync(Guid id, DateTime time, bool isActive, Actor actor)
        {
            actor.CreatedByUserId = Guid.Parse("e2a804b7-964e-4f8a-a125-2eb492cc3108");
            actor.UpdatedByUserId = Guid.Parse("e2a804b7-964e-4f8a-a125-2eb492cc3108");
            try
            {
                await connection.OpenAsync();
                using (connection)
                {
                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    command.CommandText = ($"INSERT INTO \"Actor\" (\"Id\", \"FirstName\", \"LastName\", \"Bio\", \"Image\", \"IsActive\", \"CreatedByUserId\", \"UpdatedByUserId\", \"DateCreated\", \"DateUpdated\") VALUES (@Id, @FirstName, @LastName, @Bio, @Image, @IsActive, @CreatedByUserId, @UpdatedByUserId, @DateCreated, @DateUpdated)");
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@FirstName", actor.FirstName);
                    command.Parameters.AddWithValue("@LastName", actor.LastName);
                    command.Parameters.AddWithValue("@Bio", actor.Bio);
                    command.Parameters.AddWithValue("@Image", actor.Image);
                    command.Parameters.AddWithValue("@IsActive", isActive);
                    command.Parameters.AddWithValue("@CreatedByUserId", actor.CreatedByUserId);
                    command.Parameters.AddWithValue("@UpdatedByUserId", actor.UpdatedByUserId);
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

        public async Task<bool> PutAsync(Guid id, Actor actor, DateTime time)
        {
            actor.UpdatedByUserId = Guid.Parse("e2a804b7-964e-4f8a-a125-2eb492cc3108");
            try
            {
                using (connection)
                {
                    await connection.OpenAsync();
                    StringBuilder stringBuilder = new StringBuilder();
                    NpgsqlCommand command = new NpgsqlCommand();
                    stringBuilder.Append("UPDATE \"Actor\" set ");
                    if (actor.FirstName != null)
                    {
                        stringBuilder.Append("\"FirstName\"=@firstName,");
                        command.Parameters.AddWithValue("@firstName", actor.FirstName);
                    }
                    if (actor.LastName != null)
                    {
                        stringBuilder.Append("\"LastName\"=@lastName,");
                        command.Parameters.AddWithValue("@lastName", actor.LastName);
                    }
                    if (actor.Bio != null)
                    {
                        stringBuilder.Append("\"Bio\"=@bio,");
                        command.Parameters.AddWithValue("@bio", actor.Bio);
                    }
                    if (actor.Image != null)
                    {
                        stringBuilder.Append("\"Image\"=@image,");
                        command.Parameters.AddWithValue("@image", actor.Image);
                    }
                    stringBuilder.Append(" \"UpdatedByUserId\"=@updatedByUserId,");
                    command.Parameters.AddWithValue("@updatedByUserId", actor.UpdatedByUserId);
                    stringBuilder.Append(" \"DateUpdated\"=@dateUpdated");
                    command.Parameters.AddWithValue("@dateUpdated", time);
                    stringBuilder.Append(" WHERE \"Id\"=@id;");
                    command.Parameters.AddWithValue("@id", id);
                    command.Connection = connection;
                    command.CommandText = stringBuilder.ToString();

                    if(await command.ExecuteNonQueryAsync() != -1)
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

