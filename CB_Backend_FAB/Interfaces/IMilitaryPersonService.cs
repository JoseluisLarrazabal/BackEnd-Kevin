using CB_Backend_FAB.Models;

namespace CB_Backend_FAB.Services
{
    public interface IMilitaryPersonService
    {
        Task<IEnumerable<MilitaryPerson>> GetAllAsync();
        Task<MilitaryPerson> GetByIdAsync(int id);
        Task CreateAsync(MilitaryPerson groupFAB);
    }
}
