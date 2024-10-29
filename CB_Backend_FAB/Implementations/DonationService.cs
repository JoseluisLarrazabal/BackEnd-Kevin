using CB_Backend_FAB.Models;
using CB_Backend_FAB.Services;
using MySql.Data.MySqlClient;

namespace CB_Backend_FAB.Implementations
{
    public class DonationService : IDonationService
    {

        private readonly string _connectionString;

        public DonationService(string connectionString)
        {
            _connectionString = connectionString;
        }

       /// <summary>
       /// Transacción muchos a muchos
       /// </summary>
       /// <param name="donation"></param>
       /// <returns></returns>
        public async Task CreateAsync(Donation donation)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var transaction = await connection.BeginTransactionAsync())
                {
                    try
                    {
                        int donationID;
                        using (var commandDonation = new MySqlCommand("INSERT INTO Donation (InstitutionID, StorageID, Status, RegistrationDate, UpdateDate, UserID) " +
                                                                      "VALUES (@InstitutionID, @StorageID, @Status, @RegistrationDate, @UpdateDate, @UserID)", connection, transaction))
                        {
                            commandDonation.Parameters.AddWithValue("@InstitutionID", donation.Institution?.InstitutionID ?? (object)DBNull.Value);
                            commandDonation.Parameters.AddWithValue("@StorageID", donation.Storage?.StorageID ?? (object)DBNull.Value);
                            commandDonation.Parameters.AddWithValue("@Status", 1);
                            commandDonation.Parameters.AddWithValue("@RegistrationDate", DateTime.Now);
                            commandDonation.Parameters.AddWithValue("@UpdateDate", DateTime.Now);
                            commandDonation.Parameters.AddWithValue("@UserID", 1);

                            await commandDonation.ExecuteNonQueryAsync();
                            donationID = (int)commandDonation.LastInsertedId; 
                        }

                        foreach (var detail in donation.Details)
                        {
                            using (var commandDetail = new MySqlCommand("INSERT INTO DonationDetail (DonationID, ItemID, Quantity, Status, RegistrationDate, UpdateDate, UserID) " +
                                                                        "VALUES (@DonationID, @ItemID, @Quantity, @Status, @RegistrationDate, @UpdateDate, @UserID)", connection, transaction))
                            {
                                commandDetail.Parameters.AddWithValue("@DonationID", donationID);
                                commandDetail.Parameters.AddWithValue("@ItemID", detail.Item?.ItemID ?? (object)DBNull.Value);
                                commandDetail.Parameters.AddWithValue("@Quantity", detail.Quantity);
                                commandDetail.Parameters.AddWithValue("@Status", 1);
                                commandDetail.Parameters.AddWithValue("@RegistrationDate", DateTime.Now);
                                commandDetail.Parameters.AddWithValue("@UpdateDate", DateTime.Now);
                                commandDetail.Parameters.AddWithValue("@UserID", 1);

                                await commandDetail.ExecuteNonQueryAsync();
                            }
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

        public async Task<IEnumerable<Donation>> GetAllAsync()
        {
            var donations = new List<Donation>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"select D.donationID, I.institutionID, I.name, S.storageID, S.storageName, D.registrationDate
                                 from donation D
                                 inner join institution I on I.institutionID = D.institutionID
                                 inner join storage S on S.storageID = D.storageID";

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            donations.Add(new Donation
                            {
                                DonationID = reader.GetInt32(0),
                                Institution =  new Institution { InstitutionID = reader.GetInt32(1), Name = reader.GetString(2) },
                                Storage = new Storage { StorageID = reader.GetInt32(3), StorageName = reader.GetString(4) },
                                RegistrationDate = reader.GetDateTime(5),
                            });
                        }
                    }
                }
            }
            return donations;
        }

        public async Task<Donation> GetByIdAsync(int id)
        {
            var donation = new Donation();
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string donationQuery = @"select D.donationID, I.institutionID, I.name, S.storageID, S.storageName, D.registrationDate
                                         from donation D
                                         inner join institution I on I.institutionID = D.institutionID
                                         inner join storage S on S.storageID = D.storageID
                                         where D.donationID = @ID";

                using (var command = new MySqlCommand(donationQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            donation = new Donation
                            {
                                DonationID = reader.GetInt32(0),
                                Institution = new Institution { InstitutionID = reader.GetInt32(1), Name = reader.GetString(2) },
                                Storage = new Storage { StorageID = reader.GetInt32(3), StorageName = reader.GetString(4) },
                                RegistrationDate = reader.GetDateTime(5),
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

                string donationDetailsQuery = @"select I.itemID, I.name, D.quantity
                                                from donationDetail D
                                                inner join item I on I.itemID = D.itemID
                                                where D.donationID = @ID";

                donation.Details = new List<DonationDetail>();

                using (var command = new MySqlCommand(donationDetailsQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            donation.Details.Add(new DonationDetail
                            {
                                Item = new Item { ItemID = reader.GetInt32(0), Name = reader.GetString(1) },
                                Quantity = reader.GetInt32(2)
                            });
                        }
                    }
                }
            }
            return donation;
        }
    }
}
