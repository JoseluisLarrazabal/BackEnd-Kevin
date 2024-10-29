using CB_Backend_FAB.Models;

namespace CB_Backend_FAB.Services
{
    public interface IGroupFABService
    {
        Task<IEnumerable<GroupFAB>> GetAllAsync();
        Task<GroupFAB> GetByIdAsync(int id);
        Task<GroupFAB> CreateAsync(GroupFAB groupFAB);
        Task UpdateAsync(GroupFAB groupFAB);
        Task DeleteAsync(int id);
    }

}
