using CakeZone.Services.Product.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CakeZone.Services.Product.Repository
{
    public class ProductRepository : RepositoryBase<Model.Product, ApplicationDbContext>
    {
        public ProductRepository(IRepositoryOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public override Expression<Func<ApplicationDbContext, DbSet<Model.Product>>> DataSet() => o => o.Products;
        public override Expression<Func<Model.Product, object>> Key() => o => o.ProductId;
    }
}
