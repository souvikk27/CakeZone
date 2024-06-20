using System.Linq.Expressions;
using CakeZone.Services.Inventory.Data;
using CakeZone.Services.Inventory.Model;
using Chronos.Specification;
using Microsoft.EntityFrameworkCore;

namespace CakeZone.Services.Inventory.Repository.Supplier
{
    public class SupplierRepository : RepositoryBase<Model.Supplier, ApplicationDbContext>, ISupplierRepository
    {
        public SupplierRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override Expression<Func<ApplicationDbContext, DbSet<Model.Supplier>>> DataSet() => s => s.Supplier;

        public override Expression<Func<Model.Supplier, object>> Key() => s => s.Id;
    }
}
