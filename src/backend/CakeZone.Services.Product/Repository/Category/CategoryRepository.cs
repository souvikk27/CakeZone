using System.Linq.Expressions;
using CakeZone.Services.Product.Data;
using Chronos.Specification;
using Microsoft.EntityFrameworkCore;

namespace CakeZone.Services.Product.Repository.Category;

public class CategoryRepository : RepositoryBase<Model.Category, ApplicationDbContext>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext options) : base(options)
    {
        
    }
    
    public override Expression<Func<ApplicationDbContext, DbSet<Model.Category>>> DataSet() => o => o.Categories;
    public override Expression<Func<Model.Category, object>> Key() => o => o.Id;
}