using NotIMDb.Common;
using NotIMDb.Model;
using NotIMDb.Repository.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace NotIMDb.Repository
{
    public class UserRepository : IUserRepository
    {
        private string connectionString = ConnectionStringHelper.Get();

        public async Task<string> RegisterAsync(User user)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    using (NpgsqlCommand userCmd = new NpgsqlCommand())
                    {
                        string existingUser = null;
                        userCmd.Connection = conn;
                        userCmd.CommandText = "SELECT \"Email\" FROM \"User\" WHERE \"Email\" = @email;";

                        userCmd.Parameters.AddWithValue("email", user.Email);

                        NpgsqlDataReader reader = await userCmd.ExecuteReaderAsync();

                        while (reader.Read())
                        {
                            existingUser = reader[0].ToString();
                        }
                        reader.Close();

                        if (existingUser != null)
                        {
                            return "User already exists!";
                        }

                        using (NpgsqlCommand roleCmd = new NpgsqlCommand())
                        {
                            //Set 'Guest' as starting role
                            roleCmd.Connection = conn;
                            roleCmd.CommandText = "SELECT \"Id\" FROM \"Role\" WHERE \"Title\" = 'User';";
                            NpgsqlDataReader roleReader = await roleCmd.ExecuteReaderAsync();

                            while (roleReader.Read())
                            {
                                user.RoleId = Guid.Parse(reader[0].ToString());
                            }
                            roleReader.Close();

                            using (NpgsqlCommand cmd = new NpgsqlCommand())
                            {
                                cmd.Connection = conn;
                                cmd.CommandText = "INSERT INTO \"User\" ( \"Id\", \"FirstName\", \"LastName\", \"Email\", \"DateCreated\", \"DateOfBirth\", \"Password\", \"IsActive\",\"RoleId\" )" +
                                                                "VALUES( @userId, @firstName, @lastName, @email, @dateCreated, @dateOfBirth, @password, @isActive, @roleId);";

                                cmd.Parameters.AddWithValue("userId", user.Id);
                                cmd.Parameters.AddWithValue("firstName", user.FirstName);
                                cmd.Parameters.AddWithValue("lastName", user.LastName);
                                cmd.Parameters.AddWithValue("email", user.Email);
                                cmd.Parameters.AddWithValue("dateCreated", user.DateCreated);
                                cmd.Parameters.AddWithValue("dateOfBirth", user.DateOfBirth);
                                cmd.Parameters.AddWithValue("password", user.Password);
                                cmd.Parameters.AddWithValue("isActive", true);
                                cmd.Parameters.AddWithValue("roleId", user.RoleId);

                                int RowsAffected = await cmd.ExecuteNonQueryAsync();

                                if (RowsAffected > 0)
                                {
                                    return "Registration successful!";
                                }
                                return "Bad Request";
                            };

                        }
                    }
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<User> ValidateUserAsync(User user)
        {
            string hashedPassword;
            bool passwordIsValid;
            User requestedUser = new User();

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM \"User\" WHERE \"Email\" = @email";
                    cmd.Parameters.AddWithValue("email", user.Email);

                    NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            hashedPassword = reader["Password"].ToString();
                            passwordIsValid = BCrypt.Net.BCrypt.Verify(user.Password, hashedPassword);

                            if (passwordIsValid)
                            {
                                requestedUser = new User()
                                {
                                    Id = Guid.Parse(reader["Id"].ToString()),
                                    FirstName =reader["FirstName"].ToString(),
                                    LastName =reader["LastName"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    DateCreated = reader.GetFieldValue<DateTime>(reader.GetOrdinal("DateCreated")),
                                    //DateUpdated = reader.GetFieldValue<DateTime>(reader.GetOrdinal("DateUpdated")),
                                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                                    RoleId = Guid.Parse(reader["RoleId"].ToString()),
                                };
                            }
                            else
                            {
                                requestedUser = null;
                            }
                        }
                        return requestedUser;
                    }
                    return null;
                }
            }
        }

        public async Task<Role> GetUserRoleAsync(Guid? roleId)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (NpgsqlCommand roleCmd = new NpgsqlCommand())
                    {
                        roleCmd.Connection = conn;
                        roleCmd.CommandText = "SELECT * FROM \"Role\" WHERE \"Id\" = @roleId;";
                        roleCmd.Parameters.AddWithValue("roleId", roleId);

                        NpgsqlDataReader roleReader = await roleCmd.ExecuteReaderAsync();
                        if (roleReader.HasRows)
                        {
                            while (roleReader.Read())
                            {
                                DateTime? dateUpdated;

                                if (!roleReader.IsDBNull(roleReader.GetOrdinal("DateUpdated")))
                                {
                                    dateUpdated = roleReader.GetFieldValue<DateTime>(roleReader.GetOrdinal("DateUpdated"));
                                }
                                else
                                {
                                    dateUpdated = null;
                                }

                                Role role = new Role()
                                {
                                    Id = Guid.Parse(roleReader["Id"].ToString()),
                                    Title = roleReader["Title"].ToString(),
                                    IsActive = (bool)roleReader["IsActive"],
                                    DateCreated = roleReader.GetFieldValue<DateTime>(roleReader.GetOrdinal("DateCreated")),
                                    DateUpdated = dateUpdated,
                                };
                                return role;
                            }
                        }

                        return null;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Role>> GetRoles() {
            List<Role> roles = new List<Role>();
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (NpgsqlCommand roleCmd = new NpgsqlCommand())
                    {
                        roleCmd.Connection = conn;
                        roleCmd.CommandText = "SELECT * FROM \"Role\" ";
                        //roleCmd.Parameters.AddWithValue("roleId", roleId);

                        NpgsqlDataReader roleReader = await roleCmd.ExecuteReaderAsync();

                        while (roleReader.Read())
                        {
                            DateTime? dateUpdated;

                            if (!roleReader.IsDBNull(roleReader.GetOrdinal("DateUpdated")))
                            {
                                dateUpdated = roleReader.GetFieldValue<DateTime>(roleReader.GetOrdinal("DateUpdated"));
                            }
                            else
                            {
                                dateUpdated = null;
                            }

                            Role role= new Role()
                            {
                                Id = Guid.Parse(roleReader["Id"].ToString()),
                                Title = roleReader["Title"].ToString(),
                                IsActive = (bool)roleReader["IsActive"],
                                DateCreated = roleReader.GetFieldValue<DateTime>(roleReader.GetOrdinal("DateCreated")),
                                DateUpdated = dateUpdated,
                            };
                            roles.Add(role);
                        }

                        return roles;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }


    }
}
