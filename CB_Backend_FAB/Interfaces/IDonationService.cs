using CB_Backend_FAB.Models;

namespace CB_Backend_FAB.Services
{
    public interface IDonationService
    {
        Task CreateAsync(Donation donation);

        Task<IEnumerable<Donation>> GetAllAsync();

        Task<Donation> GetByIdAsync(int id);
    }
}
