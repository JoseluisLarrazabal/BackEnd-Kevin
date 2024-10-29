using CB_Backend_FAB.Models;
using Microsoft.EntityFrameworkCore;

namespace CB_Backend_FAB.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
