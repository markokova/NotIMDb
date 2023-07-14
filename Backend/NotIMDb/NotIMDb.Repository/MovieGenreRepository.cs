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
    public class MovieGenreRepository : IMovieGenreRepository
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
                    command.CommandText = ($"UPDATE \"MovieGenre\" set \"IsActive\"=false WHERE \"Id\"=@id;");
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

        public async Task<bool> PostAsync(Guid guid, DateTime time, bool isActive, MovieGenre movieGenre)
        {
            movieGenre.CreatedByUserId = Guid.Parse("e2a804b7-964e-4f8a-a125-2eb492cc3108");
            movieGenre.UpdatedByUserId = Guid.Parse("e2a804b7-964e-4f8a-a125-2eb492cc3108");
            try
            {
                await connection.OpenAsync();
                using (connection)
                {
                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    command.CommandText = ($"INSERT INTO \"MovieGenre\" (\"Id\", \"MovieId\", \"GenreId\", \"IsActive\", \"CreatedByUserId\", \"UpdatedByUserId\", \"DateCreated\", \"DateUpdated\") VALUES (@Id, @MovieId, @GenreId, @IsActive, @CreatedByUserId, @UpdatedByUserId, @DateCreated, @DateUpdated)");
                    command.Parameters.AddWithValue("@Id", guid);
                    command.Parameters.AddWithValue("@MovieId", movieGenre.MovieId);
                    command.Parameters.AddWithValue("@GenreId", movieGenre.GenreId);
                    command.Parameters.AddWithValue("@IsActive", isActive);
                    command.Parameters.AddWithValue("@CreatedByUserId", movieGenre.CreatedByUserId);
                    command.Parameters.AddWithValue("@UpdatedByUserId", movieGenre.UpdatedByUserId);
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
    }
}
