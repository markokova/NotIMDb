using NotIMDb.Common;
using NotIMDb.Model;
using NotIMDb.Repository.Common;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NotIMDb.Repository
{
    public class WatchlistRepository : IWatchlistRepository
    {
        private string connectionString = ConnectionStringHelper.Get();

        public async Task<int> AddToWatchListAsync(Guid movieId, CurrentUser currentUser)
        {
            int affectedRows = 0;
            string createdByUserIdString = "08d58761-429b-49a3-9bce-9b7ca1b3459a";
            Guid createdByUserId = Guid.Parse(createdByUserIdString);

            string updatedByUserIdString = "08d58761-429b-49a3-9bce-9b7ca1b3459a";
            Guid updatedByUserId = Guid.Parse(updatedByUserIdString);

            Guid ids = currentUser.Id;


            string query = "INSERT INTO \"WatchList\" (\"Id\",\"IsWatched\",\"UserId\",\"MovieId\",\"IsActive\",\"CreatedByUserId\",\"UpdatedByUserId\"," +
                "\"DateCreated\",\"DateUpdated\") VALUES(@Id, @IsWatched, @UserId, @MovieId, @IsActive, @CreatedByUserId, @UpdatedByUserId," +
                " @DateCreated, @DateUpdated)";

            Guid id = Guid.NewGuid();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Parameters.AddWithValue("@Id",id);
                    command.Parameters.AddWithValue("@IsWatched", false);
                    command.Parameters.AddWithValue("@MovieId", movieId);
                    command.Parameters.AddWithValue("IsActive", true);
                    command.Parameters.AddWithValue("UserId", createdByUserId);
                    command.Parameters.AddWithValue("CreatedByUserId", createdByUserId);
                    command.Parameters.AddWithValue("UpdatedByUserId", updatedByUserId);  
                    command.Parameters.AddWithValue("DateCreated", DateTime.Now);
                    command.Parameters.AddWithValue("DateUpdated", DateTime.Now);

                    command.Connection = connection;
                    command.CommandText = query;
                    affectedRows = await command.ExecuteNonQueryAsync();
                }
            }

            return affectedRows;
        }

        public async Task<int> DeleteFromWatchListAsync(Guid id, CurrentUser currentUser)
        {
            int affectedRows = 0;
            string query = "UPDATE \"WatchList\" SET \"IsActive\" = false WHERE \"MovieId\" = @Id AND \"UserId\" = @UserId";
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using(NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@UserId", currentUser.Id);
                    command.Connection = connection;
                    command.CommandText = query;

                    affectedRows = await command.ExecuteNonQueryAsync();
                }
            }
            return affectedRows;
        }

        public async Task<int> MarkAsWatchedAsync(Guid id, CurrentUser currentUser)
        {
            int affectedRows = 0;
            string query = "UPDATE \"WatchList\" SET \"IsWatched\" = true WHERE \"MovieId\" = @Id AND \"UserId\" = @UserId";
            
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@UserId", currentUser.Id);
                    command.Connection = connection;
                    command.CommandText = query;

                    affectedRows = await command.ExecuteNonQueryAsync();
                }
            }
            return affectedRows;
        }
    }
}
