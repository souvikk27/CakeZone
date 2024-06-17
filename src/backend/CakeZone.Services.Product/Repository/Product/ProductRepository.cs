using CakeZone.Services.Product.Data;
using CakeZone.Services.Product.Shared.Attributes;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Chronos.Specification;

namespace CakeZone.Services.Product.Repository.Product
{
    public class ProductRepository : RepositoryBase<Model.Product, ApplicationDbContext>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext options) : base(options)
        {
        }

        public override Expression<Func<ApplicationDbContext, DbSet<Model.Product>>> DataSet() => o => o.Products;

        public override Expression<Func<Model.Product, object>> Key() => o => o.Id;

        public async Task<IEnumerable<Model.Product>> GetProductsWithImages(string sku) =>
            await Context.Products.Include(i => i.ProductImages)
            .Include(c => c.Categories)
            .Include(a => a.Attributes)
                .Where(p => p.Sku == sku)
                .ToListAsync();

        public async Task<bool> AddProductsWithParametersAsync(Model.Product product, Guid categoryId, IEnumerable<AttributeProductDto> attributeProducts)
        {
            var connectionString = Context.Database.GetConnectionString();

            await using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                await using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var productInsertQuery =
                            @" INSERT INTO Products (Id, Name, Sku, Description, Price,
                                        CreatedAt, UpdatedAt, IsDeleted, DeletedAt)
                                        VALUES (@Id, @Name, @Sku, @Description, @Price,
                                        @CreatedAt, @UpdatedAt, @IsDeleted, @DeletedAt)";
                        var parameters = new
                        {
                            product.Id,
                            product.Name,
                            product.Sku,
                            product.Description,
                            product.Price,
                            product.CreatedAt,
                            product.UpdatedAt,
                            product.IsDeleted,
                            product.DeletedAt
                        };

                        await connection.ExecuteAsync(productInsertQuery, parameters, transaction: transaction);

                        var categoryQuery = $@"INSERT INTO [dbo].[ProductCategories] (ProductId, CategoryId)
                            VALUES ('{product.Id}', '{categoryId}')";

                        await connection.ExecuteAsync(categoryQuery, transaction: transaction);

                        foreach (var attribute in attributeProducts)
                        {
                            var attributiveQuery = $@"INSERT INTO [dbo].[ProductAttributes] (AttributeId, ProductId, Value)
                                VALUES ('{attribute.AttributeId}', '{product.Id}', '{attribute.Value}')";
                            await connection.ExecuteAsync(attributiveQuery, transaction: transaction);
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }
    }
}