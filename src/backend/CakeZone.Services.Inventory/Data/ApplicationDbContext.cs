using CakeZone.Services.Inventory.Model;
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
        public DbSet<Model.Storage_Depot> Storage_Depot { get; set; }
        public DbSet<Model.StockIssue> StockIssue { get; set; }
        public DbSet<Model.StockReceipt> StockReceipt { get; set; }
        public DbSet<Model.Supplier> Supplier { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model.Inventory>()
                .HasKey(i => new { i.ProductId, i.StorageDepotId });

            modelBuilder.Entity<StockIssue>()
                .HasOne(si => si.Inventory)
                .WithMany()
                .HasForeignKey(si => new { si.ProductId, si.Storage_DepotId})
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
