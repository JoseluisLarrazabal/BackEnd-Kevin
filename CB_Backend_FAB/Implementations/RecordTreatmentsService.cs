using CB_Backend_FAB.Models;
using CB_Backend_FAB.Services;
using MySql.Data.MySqlClient;

namespace CB_Backend_FAB.Implementations
{
    public class RecordTreatmentsService : IRecordTreatmentsService
    {
        private readonly string _connectionString;

        public RecordTreatmentsService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task CreateAsync(RecordTreatments recordTreatments)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var transaction = await connection.BeginTransactionAsync())
                {
                    try
                    {
                        using (var command = new MySqlCommand("INSERT INTO recordtreatments (attentionDate, diagnosis, treatment, personID, status, registerDate, lastUpdate, userID) " +
                                                              "VALUES (@AttentionDate, @Diagnosis, @Treatment, @PersonID, @Status, @RegisterDate, @LastUpdate, @UserID)", connection, transaction))
                        {
                            command.Parameters.AddWithValue("@AttentionDate", recordTreatments.AttentionDate);
                            command.Parameters.AddWithValue("@Diagnosis", recordTreatments.Diagnosis);
                            command.Parameters.AddWithValue("@Treatment", recordTreatments.Treatment);
                            command.Parameters.AddWithValue("@PersonID", recordTreatments.Person?.PersonID);
                            command.Parameters.AddWithValue("@Status", recordTreatments.Status);
                            command.Parameters.AddWithValue("@RegisterDate", recordTreatments.RegisterDate);
                            command.Parameters.AddWithValue("@LastUpdate", recordTreatments.LastUpdate);
                            command.Parameters.AddWithValue("@UserID", recordTreatments.UserID);

                            await command.ExecuteNonQueryAsync();
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

        public async Task<IEnumerable<RecordTreatments>> GetAllAsync()
        {
            var recordTreatmentsList = new List<RecordTreatments>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"SELECT recordTreatmentsID, attentionDate, diagnosis, treatment, personID, status, registerDate, lastUpdate, userID
                                 FROM recordtreatments";

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            recordTreatmentsList.Add(new RecordTreatments
                            {
                                RecordTreatmentsID = reader.GetInt32(0),
                                AttentionDate = reader.GetDateTime(1),
                                Diagnosis = reader.GetString(2),
                                Treatment = reader.GetString(3),
                                Person = new Person
                                {
                                    PersonID = reader.GetInt32(4),
                                },
                                Status = reader.GetByte(5),
                                RegisterDate = reader.GetDateTime(6),
                                LastUpdate = reader.GetDateTime(7),
                                UserID = reader.GetInt16(8)
                            });
                        }
                    }
                }
            }
            return recordTreatmentsList;
        }

        public async Task<RecordTreatments> GetByIdAsync(int id)
        {
            RecordTreatments recordTreatments = null;
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"SELECT recordTreatmentsID, attentionDate, diagnosis, treatment, personID, status, registerDate, lastUpdate, userID
                                 FROM recordtreatments
                                 WHERE recordTreatmentsID = @ID";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            recordTreatments = new RecordTreatments
                            {
                                RecordTreatmentsID = reader.GetInt32(0),
                                AttentionDate = reader.GetDateTime(1),
                                Diagnosis = reader.GetString(2),
                                Treatment = reader.GetString(3),
                                Person = new Person
                                {
                                    PersonID = reader.GetInt32(4),
                                },
                                Status = reader.GetByte(5),
                                RegisterDate = reader.GetDateTime(6),
                                LastUpdate = reader.GetDateTime(7),
                                UserID = reader.GetInt16(8)
                            };
                        }
                    }
                }
            }
            return recordTreatments;
        }
    }
}
