using System.Linq.Expressions;
using CakeZone.Common.Repository;
using CakeZone.Services.Product.Data;
using Microsoft.EntityFrameworkCore;

namespace CakeZone.Services.Product.Repository.Category;

public class CategoryRepository : RepositoryBase<Model.Category, ApplicationDbContext>, ICategoryRepository
{
    public CategoryRepository(IRepositoryOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public override Expression<Func<ApplicationDbContext, DbSet<Model.Category>>> DataSet() => o => o.Categories;
    public override Expression<Func<Model.Category, object>> Key() => o => o.CategoryId;
}