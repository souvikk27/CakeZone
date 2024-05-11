using System.Linq.Expressions;
using CakeZone.Services.Product.Data;
using Microsoft.EntityFrameworkCore;

namespace CakeZone.Services.Product.Repository.Attribute;

public class AttributeRepository : RepositoryBase<Model.Attribute, ApplicationDbContext>, IAttributeRepository
{
    public AttributeRepository(IRepositoryOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public override Expression<Func<ApplicationDbContext, DbSet<Model.Attribute>>> DataSet() => o => o.Attributes;
    public override Expression<Func<Model.Attribute, object>> Key() => o => o.AttributeId;
}