using NotIMDb.Common;
using NotIMDb.Model;
using NotIMDb.Repository.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Repository
{
    public class UserManagementRepository : IUserManagementRepository
    {
        private string connectionString = ConnectionStringHelper.Get();

        public async Task<PaginatedResponse<User>> GetAllUsers(Sorting sorting, Paging paging, UserFiltering userFiltering)
        {
            List<User> users = new List<User>();
            List<string> errors = new List<string>();
            int totalcount = 0;

            string myQuery = "FROM \"User\" u ";

            StringBuilder queryBuilder = new StringBuilder("SELECT * " + myQuery);

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        queryBuilder = FilterResults(queryBuilder, userFiltering);
                        queryBuilder = SortResults(queryBuilder, sorting);
                        queryBuilder = PageResults(queryBuilder);


                        command.Connection = connection;
                        command.CommandText = queryBuilder.ToString();

                        command.Parameters.AddWithValue("@OffsetValue", (paging.CurrentPage - 1) * (paging.PageSize - 1));
                        command.Parameters.AddWithValue("@LimitValue", paging.PageSize);
                        command.Parameters.AddWithValue("@FirstName", $"%{userFiltering.FirstName}%" ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@LastName", $"%{userFiltering.LastName}%" ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Email", $"%{userFiltering.Email}%" ?? (object)DBNull.Value);

                        NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                DateTime? dateUpdated;

                                if (!reader.IsDBNull(reader.GetOrdinal("DateUpdated")))
                                {
                                    dateUpdated = reader.GetFieldValue<DateTime>(reader.GetOrdinal("DateUpdated"));
                                }
                                else
                                {
                                    dateUpdated = null;
                                }

                                User user = new User()
                                {
                                    Id = Guid.Parse(reader["Id"].ToString()),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    DateOfBirth = reader.GetFieldValue<DateTime>(reader.GetOrdinal("DateOfBirth")),
                                    IsActive = (bool)reader["IsActive"],
                                    DateCreated = reader.GetFieldValue<DateTime>(reader.GetOrdinal("DateCreated")),
                                    DateUpdated = dateUpdated,
                                    RoleId = Guid.Parse(reader["RoleId"].ToString()),
                                };
                                users.Add(user);
                            }
                        }
                        reader.Close();
                    }

                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = $"SELECT COUNT(*) FROM \"User\"";
                        NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                        while (reader.Read())
                        {
                            totalcount = reader.GetInt16(0);
                        }
                        reader.Close();
                    }

                    if (users.Count() == 0)
                    {
                        errors.Add("USER_NOT_FOUND");
                    }
                    PaginatedResponse<User> usersPaged = new PaginatedResponse<User>()
                    {
                        Errors = errors,
                        CurrentPage = paging.CurrentPage,
                        PageSize = paging.PageSize,
                        TotalCount = totalcount,
                        TotalPages = (totalcount + paging.PageSize - 1) / paging.PageSize,
                        Results = users
                    };

                    return usersPaged;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ResponseBaseModel<User>> GetById(Guid id)
        {
            User user = new User();
            List<string> errors = new List<string>();
            ResponseBaseModel<User> result = new ResponseBaseModel<User>();

            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand();

                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM \"User\" WHERE \"Id\" = @userGuid";

                    cmd.Parameters.AddWithValue("userGuid", id);

                    NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            DateTime? dateUpdated;

                            if (!reader.IsDBNull(reader.GetOrdinal("DateUpdated")))
                            {
                                dateUpdated = reader.GetFieldValue<DateTime>(reader.GetOrdinal("DateUpdated"));
                            }
                            else
                            {
                                dateUpdated = null;
                            }

                            user = new User()
                            {
                                Id = Guid.Parse(reader["Id"].ToString()),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString(),
                                DateOfBirth = reader.GetFieldValue<DateTime>(reader.GetOrdinal("DateOfBirth")),
                                IsActive = (bool)reader["IsActive"],
                                DateCreated = reader.GetFieldValue<DateTime>(reader.GetOrdinal("DateCreated")),
                                DateUpdated = dateUpdated,
                                RoleId = Guid.Parse(reader["RoleId"].ToString()),
                            };
                        }
                        result.Result = user;
                    }
                    else
                    {
                        errors.Add("USER_NOT_FOUND");
                        result.Errors = errors;
                    }

                    return result;
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        public async Task<ResponseBaseModel<bool>> Edit(Guid id, User request, CurrentUser currentUser)
        {
            User user = new User();
            List<string> errors = new List<string>();

            ResponseBaseModel<bool> result = new ResponseBaseModel<bool>();
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    using (NpgsqlCommand selectCmd = new NpgsqlCommand())
                    {
                        selectCmd.Connection = conn;
                        selectCmd.CommandText = "SELECT * FROM \"User\" WHERE \"Id\" = @userGuid";

                        selectCmd.Parameters.AddWithValue("userGuid", id);


                        using (NpgsqlDataReader reader = await selectCmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    DateTime? dateUpdated;

                                    if (!reader.IsDBNull(reader.GetOrdinal("DateUpdated")))
                                    {
                                        dateUpdated = reader.GetFieldValue<DateTime>(reader.GetOrdinal("DateUpdated"));
                                    }
                                    else
                                    {
                                        dateUpdated = null;
                                    }

                                    user = new User()
                                    {
                                        Id = Guid.Parse(reader["Id"].ToString()),
                                        FirstName = reader["FirstName"].ToString(),
                                        LastName = reader["LastName"].ToString(),
                                        Email = reader["Email"].ToString(),
                                        DateOfBirth = reader.GetFieldValue<DateTime>(reader.GetOrdinal("DateOfBirth")),
                                        IsActive = (bool)reader["IsActive"],
                                        DateCreated = reader.GetFieldValue<DateTime>(reader.GetOrdinal("DateCreated")),
                                        DateUpdated = dateUpdated,
                                        RoleId = Guid.Parse(reader["RoleId"].ToString()),
                                    };
                                }
                                reader.Close();

                                StringBuilder queryBuilder = new StringBuilder("UPDATE \"User\" SET ");

                                using (NpgsqlCommand updateCmd = new NpgsqlCommand())
                                {
                                    updateCmd.Connection = conn;

                                    if (user.FirstName != request.FirstName)
                                    {
                                        if (!string.IsNullOrEmpty(request.FirstName))
                                        {
                                            queryBuilder.Append("\"FirstName\" = @firstName, ");
                                            updateCmd.Parameters.AddWithValue("firstName", request.FirstName);

                                        }
                                        else
                                        {
                                            errors.Add("FIRST_NAME_EMPTY");
                                        }
                                    }
                                    if (user.LastName != request.LastName)
                                    {
                                        if (!string.IsNullOrEmpty(request.LastName))
                                        {
                                            queryBuilder.Append("\"LastName\" = @lastName, ");
                                            updateCmd.Parameters.AddWithValue("lastName", request.LastName);
                                        }
                                        else
                                        {
                                            errors.Add("LAST_NAME_EMPTY");
                                        }
                                    }
                                    if (user.Email != request.Email)
                                    {
                                        if (!string.IsNullOrEmpty(request.Email))
                                        {
                                            queryBuilder.Append("\"Email\" = @email, ");
                                            updateCmd.Parameters.AddWithValue("firstName", request.Email);
                                        }
                                        else
                                        {
                                            errors.Add("EMAIL_EMPTY");
                                        }
                                    }
                                    if (user.DateOfBirth != request.DateOfBirth)
                                    {
                                        if (request.DateOfBirth.HasValue)
                                        {
                                            queryBuilder.Append("\"DateOfBirth\" = @dateOfBirth, ");
                                            updateCmd.Parameters.AddWithValue("dateOfBirth", request.DateOfBirth);
                                        }
                                        else
                                        {
                                            errors.Add("DOB_EMPTY");
                                        }
                                    }
                                    if (user.IsActive != request.IsActive && currentUser.Role == "Administrator"  && user.Id != currentUser.Id)
                                    {
                                        if (request.IsActive.HasValue)
                                        {
                                            queryBuilder.Append("\"IsActive\" = @isActive, ");
                                            updateCmd.Parameters.AddWithValue("isActive", request.IsActive);
                                        }
                                        else
                                        {
                                            errors.Add("ISACTIVE_EMPTY");
                                        }
                                    }
                                    if (user.RoleId != request.RoleId && currentUser.Role == "Administrator" && user.Id != currentUser.Id)
                                    {
                                        if (request.RoleId.HasValue)
                                        {
                                            queryBuilder.Append("\"RoleId\" = @roleId, ");
                                            updateCmd.Parameters.AddWithValue("roleId", request.RoleId);
                                        }
                                        else
                                        {
                                            errors.Add("ROLEID_EMPTY");
                                        }
                                    }

                                    if (errors.Count() == 0)
                                    {
                                        queryBuilder.Append("\"DateUpdated\" = @dateUpdated, ");
                                        updateCmd.Parameters.AddWithValue("dateUpdated", request.DateUpdated);

                                        queryBuilder.Append("\"UpdatedByUserId\" = @updatedByUserId ");
                                        updateCmd.Parameters.AddWithValue("updatedByUserId", request.UpdatedByUserId);

                                        queryBuilder.Append("WHERE \"Id\" = @Id");
                                        updateCmd.Parameters.AddWithValue("@Id", id);

                                        updateCmd.CommandText = queryBuilder.ToString();

                                        int rowsAffected = await updateCmd.ExecuteNonQueryAsync();


                                        if (rowsAffected > 0)
                                        {
                                            result.Result = true;
                                        }

                                    }
                                    else
                                    {
                                        result.Errors = errors;
                                        result.Result = false;
                                    }
                                }
                            }
                            else
                            {
                                errors.Add("USER_NOT_FOUND");
                                result.Errors = errors;
                                result.Result = false;
                            }
                            return result;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private StringBuilder FilterResults(StringBuilder builder, UserFiltering userFiltering)
        {
            builder.Append(" WHERE 1 = 1");

            if (userFiltering != null)
            {
                if (!string.IsNullOrEmpty(userFiltering.FirstName))
                {
                    builder.Append(" AND u.\"FirstName\" ILIKE @FirstName ");

                }
                if (!string.IsNullOrEmpty(userFiltering.LastName))
                {
                    builder.Append(" AND u.\"LastName\" ILIKE @LastName");

                }
                if (!string.IsNullOrEmpty(userFiltering.Email))
                {
                    builder.Append(" AND u.\"Email\" ILIKE @Email");

                }
            }
            return builder;
        }

        private StringBuilder SortResults(StringBuilder builder, Sorting sorting)
        {
            builder.Append($" ORDER BY u.\"{sorting.Orderby}\" ");
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
