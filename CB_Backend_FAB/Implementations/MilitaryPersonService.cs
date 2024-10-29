using CB_Backend_FAB.Services;
using CB_Backend_FAB.Models;
using MySql.Data.MySqlClient;

namespace CB_Backend_FAB.Implementations
{
    public class MilitaryPersonService : IMilitaryPersonService
    {
        private readonly string _connectionString;

        public MilitaryPersonService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task CreateAsync(MilitaryPerson militaryPerson)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var transaction = await connection.BeginTransactionAsync())
                {
                    try
                    {
                        int personID;
                        if (militaryPerson.Person != null)
                        {
                            using (var commandPerson = new MySqlCommand("INSERT INTO Person (Name, Lastname, Ci, Birthday, Status, RegisterDate, LastUpdate, UserID) " +
                                                                        "VALUES (@Name, @Lastname, @Ci, @Birthday, @Status, @RegisterDate, @LastUpdate, @UserID)", connection, transaction))
                            {
                                commandPerson.Parameters.AddWithValue("@Name", militaryPerson.Person.Name);
                                commandPerson.Parameters.AddWithValue("@Lastname", militaryPerson.Person.Lastname);
                                commandPerson.Parameters.AddWithValue("@Ci", militaryPerson.Person.Ci);
                                commandPerson.Parameters.AddWithValue("@Birthday", militaryPerson.Person.Birthday);
                                commandPerson.Parameters.AddWithValue("@Status", 1);
                                commandPerson.Parameters.AddWithValue("@RegisterDate", DateTime.Now);
                                commandPerson.Parameters.AddWithValue("@LastUpdate", DateTime.Now);
                                commandPerson.Parameters.AddWithValue("@UserID", 1);

                                await commandPerson.ExecuteNonQueryAsync();
                                personID = (int)commandPerson.LastInsertedId;
                            }
                        }
                        else
                        {
                            throw new ArgumentException("Person cannot be null");
                        }

                        using (var commandMilitaryPerson = new MySqlCommand("INSERT INTO MilitaryPerson (militaryPersonID, Area, Status, RegisterDate, LastUpdate, UserID) " +
                                                                           "VALUES (@MilitaryPersonID, @Area, @Status, @RegisterDate, @LastUpdate, @UserID)", connection, transaction))
                        {
                            commandMilitaryPerson.Parameters.AddWithValue("@MilitaryPersonID", personID);
                            commandMilitaryPerson.Parameters.AddWithValue("@Area", militaryPerson.Area);
                            commandMilitaryPerson.Parameters.AddWithValue("@Status", 1);
                            commandMilitaryPerson.Parameters.AddWithValue("@RegisterDate", DateTime.Now);
                            commandMilitaryPerson.Parameters.AddWithValue("@LastUpdate", DateTime.Now);
                            commandMilitaryPerson.Parameters.AddWithValue("@UserID", 1);

                            await commandMilitaryPerson.ExecuteNonQueryAsync();
                        }

                        await transaction.CommitAsync();
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
        }

        public async Task<IEnumerable<MilitaryPerson>> GetAllAsync()
        {
            var militaryPersons = new List<MilitaryPerson>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"SELECT P.personID, P.name, P.lastName, P.ci, P.birthday, M.area
                                FROM person P
                                inner join militaryPerson M on P.personID = M.militaryPersonID";    

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            militaryPersons.Add(new MilitaryPerson
                            {
                                Person = new Person(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetDateTime(4), 0),
                                Area = reader.GetString(5)
                            });
                        }
                    }
                }
            }
            return militaryPersons;
        }

        public async Task<MilitaryPerson> GetByIdAsync(int id)
        {
            var militaryPersons = new MilitaryPerson();
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"SELECT P.personID, P.name, P.lastName, P.ci, P.birthday, M.area
                                FROM person P
                                inner join militaryPerson M on P.personID = M.militaryPersonID
                                WHERE P.PersonID = @personID";


                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@personID", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            militaryPersons = (new MilitaryPerson
                            {
                                Person = new Person(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetDateTime(4), 0),
                                Area = reader.GetString(5)
                            });
                        }
                    }
                }
            }
            return militaryPersons;
        }
    }
}
