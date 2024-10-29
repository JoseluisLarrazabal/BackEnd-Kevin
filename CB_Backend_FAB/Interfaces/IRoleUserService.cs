using CB_Backend_FAB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CB_Backend_FAB.Services
{
    public interface IRoleUserService
    {
        Task<IEnumerable<RoleUser>> GetAllAsync();
        Task<RoleUser> GetByIdAsync(int id);
        Task<RoleUser> CreateAsync(RoleUser roleUser);
        Task UpdateAsync(RoleUser roleUser);
        Task DeleteAsync(int id);
    }
}
