using CB_Backend_FAB.Models;
using CB_Backend_FAB.Services;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CB_Backend_FAB.Implementations
{
    public class GroupFABService : IGroupFABService
    {
        private readonly string _connectionString;

        public GroupFABService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<GroupFAB>> GetAllAsync()
        {
            var groups = new List<GroupFAB>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("SELECT * FROM GroupFAB WHERE status=1", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            groups.Add(new GroupFAB
                            {
                                GroupID = reader.GetInt32(0),
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
            return groups;
        }

        public async Task<GroupFAB> GetByIdAsync(int id)
        {
            GroupFAB group = null;
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("SELECT * FROM GroupFAB WHERE GroupID = @GroupID AND status=1", connection))
                {
                    command.Parameters.AddWithValue("@GroupID", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            group = new GroupFAB
                            {
                                GroupID = reader.GetInt32(0),
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
            return group;
        }

        public async Task<GroupFAB> CreateAsync(GroupFAB groupFAB)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("INSERT INTO GroupFAB (Name, Description) VALUES (@Name, @Description)", connection))
                {
                    command.Parameters.AddWithValue("@Name", groupFAB.Name);
                    command.Parameters.AddWithValue("@Description", groupFAB.Description);

                    await command.ExecuteNonQueryAsync();
                    groupFAB.GroupID = (int)command.LastInsertedId;
                }
            }
            return groupFAB;
        }

        public async Task UpdateAsync(GroupFAB groupFAB)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("UPDATE GroupFAB SET Name = @Name, Description = @Description WHERE GroupID = @GroupID", connection))
                {
                    command.Parameters.AddWithValue("@GroupID", groupFAB.GroupID);
                    command.Parameters.AddWithValue("@Name", groupFAB.Name);
                    command.Parameters.AddWithValue("@Description", groupFAB.Description);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("UPDATE GroupFAB SET status = 0 WHERE GroupID = @GroupID", connection))
                {
                    command.Parameters.AddWithValue("@GroupID", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
