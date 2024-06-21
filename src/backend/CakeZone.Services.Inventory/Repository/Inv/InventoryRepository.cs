using System.Linq.Expressions;
using CakeZone.Services.Inventory.Data;
using Chronos.Specification;
using Microsoft.EntityFrameworkCore;

namespace CakeZone.Services.Inventory.Repository.Inv
{
    public class InventoryRepository : RepositoryBase<Model.Inventory, ApplicationDbContext>, IInventoryRepository
    {
        public InventoryRepository(ApplicationDbContext options) : base(options)
        {
        }

        public override Expression<Func<ApplicationDbContext, DbSet<Model.Inventory>>> DataSet() => o => o.Inventory;

        public override Expression<Func<Model.Inventory, object>> Key() => o => new { o.ProductId, o.StorageDepotId };

        public async Task<Model.Inventory> GetByStorageDepotIdAndProductIdAsync(Guid storageDepotId, Guid productId)
        {
            return await Context.Inventory.AsNoTracking().FirstOrDefaultAsync(i =>
                i.StorageDepotId == storageDepotId && i.ProductId == productId);
        }
    }
}
