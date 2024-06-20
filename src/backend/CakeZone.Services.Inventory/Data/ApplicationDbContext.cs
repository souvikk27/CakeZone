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
        public DbSet<Model.StorageDepot> StorageDepot { get; set; }
        public DbSet<Model.StockIssue> StockIssue { get; set; }
        public DbSet<Model.StockReceipt> StockReceipt { get; set; }
        public DbSet<Model.Supplier> Supplier { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Model.Inventory>()
                .HasKey(i => new { i.ProductId, i.StorageDepotId });

            // StorageDepot indexes
            modelBuilder.Entity<StorageDepot>()
                .HasIndex(sd => sd.Name)
                .HasDatabaseName("IX_StorageDepot_Name");

            modelBuilder.Entity<StorageDepot>()
                .HasIndex(sd => sd.Address)
                .HasDatabaseName("IX_StorageDepot_Address");

            // Inventory indexes
            modelBuilder.Entity<Model.Inventory>()
                .HasIndex(i => i.ProductId)
                .HasDatabaseName("IX_Inventory_ProductId");

            modelBuilder.Entity<Model.Inventory>()
                .HasIndex(i => i.StorageDepotId)
                .HasDatabaseName("IX_Inventory_StorageDepotId");

            modelBuilder.Entity<Model.Inventory>()
                .HasIndex(i => i.CurrentLevel)
                .HasDatabaseName("IX_Inventory_CurrentLevel");

            modelBuilder.Entity<Model.Inventory>()
                .HasIndex(i => i.MaxLevel)
                .HasDatabaseName("IX_Inventory_MaxLevel");

            modelBuilder.Entity<Model.Inventory>()
                .HasIndex(i => i.MinLevel)
                .HasDatabaseName("IX_Inventory_MinLevel");

            // StockIssue indexes
            modelBuilder.Entity<StockIssue>()
                .HasIndex(si => si.ProductId)
                .HasDatabaseName("IX_StockIssue_ProductId");

            modelBuilder.Entity<StockIssue>()
                .HasIndex(si => si.StorageDepotId)
                .HasDatabaseName("IX_StockIssue_StorageDepotId");

            modelBuilder.Entity<StockIssue>()
                .HasIndex(si => si.IssueDate)
                .HasDatabaseName("IX_StockIssue_IssueDate");

            // StockReceipt indexes
            modelBuilder.Entity<StockReceipt>()
                .HasIndex(sr => sr.SupplierId)
                .HasDatabaseName("IX_StockReceipt_SupplierId");

            modelBuilder.Entity<StockReceipt>()
                .HasIndex(sr => sr.ProductId)
                .HasDatabaseName("IX_StockReceipt_ProductId");

            modelBuilder.Entity<StockReceipt>()
                .HasIndex(sr => sr.StorageDepotId)
                .HasDatabaseName("IX_StockReceipt_StorageDepotId");

            modelBuilder.Entity<StockReceipt>()
                .HasIndex(sr => sr.ReceiptDate)
                .HasDatabaseName("IX_StockReceipt_ReceiptDate");

            // Supplier indexes
            modelBuilder.Entity<Supplier>()
                .HasIndex(s => s.Name)
                .HasDatabaseName("IX_Supplier_Name");

            modelBuilder.Entity<Supplier>()
                .HasIndex(s => s.Email)
                .HasDatabaseName("IX_Supplier_Email");

            modelBuilder.Entity<Supplier>()
                .HasIndex(s => s.Phone)
                .HasDatabaseName("IX_Supplier_Phone");

            modelBuilder.Entity<Supplier>()
                .HasIndex(s => s.Address)
                .HasDatabaseName("IX_Supplier_Address");
        }

    }
}
