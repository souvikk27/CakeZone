using CakeZone.Services.Inventory.Repository.Inv;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.Inventory
{
    public class GetInventoryByProductIdAndStorageIdQueryHandler : IRequestHandler<GetInventoryByProductIdAndStorageIdQuery, Model.Inventory>
    {
        private readonly IInventoryRepository _inventoryRepository;

        public GetInventoryByProductIdAndStorageIdQueryHandler(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<Model.Inventory> Handle(GetInventoryByProductIdAndStorageIdQuery request, CancellationToken cancellationToken)
        {
            var inventory = await _inventoryRepository.GetByStorageDepotIdAndProductIdAsync(request.StoreDepotId, request.ProductId);
            if (inventory == null)
            {
                throw new Exception("Inventory not found or validate the id");
            }
            return inventory;
        }
    }
}
