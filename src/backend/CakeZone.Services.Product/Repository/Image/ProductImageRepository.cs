using CakeZone.Services.Product.Data;
using CakeZone.Services.Product.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Chronos.Specification;

namespace CakeZone.Services.Product.Repository.Image
{
    public class ProductImageRepository : RepositoryBase<ProductImage, ApplicationDbContext> , IProductImageRepository
    {
        public ProductImageRepository(ApplicationDbContext repositoryOptions) : base(repositoryOptions)
        {
            
        }

        public override Expression<Func<ApplicationDbContext, DbSet<ProductImage>>> DataSet() => o => o.ProductImages;
        public override Expression<Func<ProductImage, object>> Key() => o => o.Id;
    }
}
