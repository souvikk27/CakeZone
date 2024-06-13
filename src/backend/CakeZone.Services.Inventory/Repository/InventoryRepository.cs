using Ardalis.Specification.EntityFrameworkCore;
using CakeZone.Services.Inventory.Data;

namespace CakeZone.Services.Inventory.Repository
{
    public class InventoryRepository : RepositoryBase<Model.Inventory>, IInventoryRepository
    {
        public InventoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
