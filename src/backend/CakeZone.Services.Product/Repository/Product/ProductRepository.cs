using System.Linq.Expressions;
using CakeZone.Services.Product.Data;
using Microsoft.EntityFrameworkCore;

namespace CakeZone.Services.Product.Repository.Product
{
    public class ProductRepository : RepositoryBase<Model.Product, ApplicationDbContext>, IProductRepository
    {
        public ProductRepository(IRepositoryOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public override Expression<Func<ApplicationDbContext, DbSet<Model.Product>>> DataSet() => o => o.Products;

        public override Expression<Func<Model.Product, object>> Key() => o => o.Id;

        public async Task<IEnumerable<Model.Product>> GetProductsWithImages(string sku)
        {
            return await Context.Products.Include(i => i.ProductImages).Where(p => p.Sku == sku).ToListAsync();
        }
    }
}