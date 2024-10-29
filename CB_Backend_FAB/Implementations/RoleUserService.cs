using CB_Backend_FAB.Models;
using CB_Backend_FAB.Services;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CB_Backend_FAB.Implementations
{
    public class RoleUserService : IRoleUserService
    {
        private readonly string _connectionString;

        public RoleUserService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<RoleUser>> GetAllAsync()
        {
            var roles = new List<RoleUser>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("SELECT * FROM RoleUser WHERE Status = 1", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            roles.Add(new RoleUser
                            {
                                RoleID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                Status = reader.GetByte(3),
                                RegistrationDate = reader.GetDateTime(4),
                                LastUpdate = reader.GetDateTime(5)
                            });
                        }
                    }
                }
            }
            return roles;
        }

        public async Task<RoleUser> GetByIdAsync(int id)
        {
            RoleUser role = null;
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("SELECT * FROM RoleUser WHERE RoleID = @RoleID AND Status = 1", connection))
                {
                    command.Parameters.AddWithValue("@RoleID", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            role = new RoleUser
                            {
                                RoleID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                Status = reader.GetByte(3),
                                RegistrationDate = reader.GetDateTime(4),
                                LastUpdate = reader.GetDateTime(5)
                            };
                        }
                    }
                }
            }
            return role;
        }

        public async Task<RoleUser> CreateAsync(RoleUser roleUser)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("INSERT INTO RoleUser (Name, Description) VALUES (@Name, @Description)", connection))
                {
                    command.Parameters.AddWithValue("@Name", roleUser.Name);
                    command.Parameters.AddWithValue("@Description", roleUser.Description);

                    await command.ExecuteNonQueryAsync();
                    roleUser.RoleID = (int)command.LastInsertedId;
                }
            }
            return roleUser;
        }

        public async Task UpdateAsync(RoleUser roleUser)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("UPDATE RoleUser SET Name = @Name, Description = @Description WHERE RoleID = @RoleID", connection))
                {
                    command.Parameters.AddWithValue("@RoleID", roleUser.RoleID);
                    command.Parameters.AddWithValue("@Name", roleUser.Name);
                    command.Parameters.AddWithValue("@Description", roleUser.Description);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("UPDATE RoleUser SET Status = 0 WHERE RoleID = @RoleID", connection))
                {
                    command.Parameters.AddWithValue("@RoleID", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
