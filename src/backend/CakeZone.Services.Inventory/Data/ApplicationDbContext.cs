using Microsoft.EntityFrameworkCore;

namespace CakeZone.Services.Inventory.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }   

        public DbSet<Model.Inventory>? Inventory { get; set; }
    }
}
