using CakeZone.Services.Product.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Attribute = CakeZone.Services.Product.Model.Attribute;

namespace CakeZone.Services.Product.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Model.Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Attribute> Attributes { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Model.Product>(BuildProductAction);

            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany()
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void BuildProductAction(EntityTypeBuilder<Model.Product> entity)
        {
            entity.ToTable("Products");

            entity.HasKey(e => e.ProductId).HasName("PK_Products_5793275AF5B5B6D7");

            entity.Property(e => e.ProductId)
                .HasDefaultValueSql("newid()")
                .HasColumnName("ProductId");

            entity.HasMany(c => c.Categories)
                .WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>("ProductCategories", r => r.HasOne<Category>()
                    .WithMany()
                    .HasForeignKey("CategoryId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_G798FB98899322"), s => s.HasOne<Model.Product>()
                    .WithMany()
                    .HasForeignKey("ProductId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_5793275AF5B5B6D7"), j =>
                    {
                        j.HasKey("ProductId", "CategoryId");
                        j.ToTable("ProductCategories");
                        j.IndexerProperty<Guid>("ProductId").HasColumnName("ProductId");
                        j.IndexerProperty<Guid>("CategoryId").HasColumnName("CategoryId");
                    });

            entity.HasMany(c => c.Attributes)
                .WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>("ProductAttributes", r => r.HasOne<Attribute>()
                    .WithMany()
                    .HasForeignKey("AttributeId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Attributes_5683275AF5B5B6D7"), s => s.HasOne<Model.Product>()
                    .WithMany()
                    .HasForeignKey("ProductId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_5793275AF5B5B69F"), j =>
                    {
                        j.HasKey("AttributeId", "ProductId");
                        j.ToTable("ProductAttributes");
                        j.IndexerProperty<Guid>("AttributeId").HasColumnName("AttributeId");
                        j.IndexerProperty<Guid>("ProductId").HasColumnName("ProductId");
                        j.Property<string>("Value").HasColumnName("Value");
                    });

            entity.HasMany(p => p.ProductImages)
                .WithOne(pi => pi.Product)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}