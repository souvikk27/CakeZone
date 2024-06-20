using System.Linq.Expressions;
using CakeZone.Services.Inventory.Data;
using CakeZone.Services.Inventory.Model;
using Chronos.Specification;
using Microsoft.EntityFrameworkCore;
namespace CakeZone.Services.Inventory.Repository.Depot
{
    public class StorageDepotRepository : RepositoryBase<StorageDepot, ApplicationDbContext>, IStorageDepotRepository
    {
        public StorageDepotRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override Expression<Func<ApplicationDbContext, DbSet<StorageDepot>>> DataSet() => m => m.StorageDepot;
        public override Expression<Func<StorageDepot , object>> Key() => m => m.Id;
    }
}
