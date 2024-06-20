using Chronos.Specification;

namespace CakeZone.Services.Inventory.Repository.Inv
{
    public interface IInventoryRepository : IRepositoryBase<Model.Inventory>
    {
        Task<Model.Inventory> GetByStorageDepotIdAndProductIdAsync(Guid storageDepotId, Guid productId);
    }
}
