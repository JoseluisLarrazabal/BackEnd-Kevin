using CB_Backend_FAB.Models;
using CB_Backend_FAB.Services;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CB_Backend_FAB.Implementations
{
    public class UserService : IUserService
    {
        private readonly string _connectionString;

        public UserService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = new List<User>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("SELECT * FROM Users WHERE status=1", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            users.Add(new User
                            {
                                UserID = reader.GetInt32("UserID"),
                                Email = reader.GetString("Email"),
                                Password = reader.GetString("Password"),
                                Status = reader.GetByte("Status"),
                                RegistrationDate = reader.GetDateTime("RegistrationDate"),
                                LastUpdate = reader.GetDateTime("LastUpdate"),
                                Role = new RoleUser( reader.GetInt32("RoleID"), "", ""),
                                Group = new GroupFAB( reader.GetInt32("GroupID"), "", "")
                            });
                        }
                    }
                }
            }
            return users;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            User user = null;
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("SELECT * FROM Users WHERE UserID = @UserID AND status=1", connection))
                {
                    command.Parameters.AddWithValue("@UserID", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            user = new User
                            {
                                UserID = reader.GetInt32("UserID"),
                                Email = reader.GetString("Email"),
                                Password = reader.GetString("Password"),
                                Status = reader.GetByte("Status"),
                                RegistrationDate = reader.GetDateTime("RegistrationDate"),
                                LastUpdate = reader.GetDateTime("LastUpdate"),
                                Role = new RoleUser(reader.GetInt32("RoleID"), "", ""),
                                Group = new GroupFAB(reader.GetInt32("GroupID"), "", "")
                            };
                        }
                    }
                }
            }
            return user;
        }

        public async Task<User> CreateAsync(User user)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("INSERT INTO Users (Email, Password, RoleID, GroupID) VALUES ( @Email, @Password, @RoleID, @GroupID)", connection))
                {
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@RoleID", user.Role.RoleID);
                    command.Parameters.AddWithValue("@GroupID", user.Group.GroupID);

                    await command.ExecuteNonQueryAsync();
                    user.UserID = (int)command.LastInsertedId;
                }
            }
            return user;
        }

        public async Task UpdateAsync(User user)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("UPDATE Users SET Email = @Email, Password = @Password, RoleID = @RoleID, GroupID = @GroupID WHERE UserID = @UserID", connection))
                {
                    command.Parameters.AddWithValue("@UserID", user.UserID);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@RoleID", user.Role.RoleID);
                    command.Parameters.AddWithValue("@GroupID", user.Group.GroupID);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("UPDATE Users SET status=0 WHERE UserID = @UserID", connection))
                {
                    command.Parameters.AddWithValue("@UserID", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
