using CakeZone.Services.Inventory.Repository.Inv;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.Inventory
{
    public class DeleteInventoryCommandHandler : IRequestHandler<DeleteInventoryCommand, Model.Inventory>
    {
        private readonly IInventoryRepository _inventoryService;

        public DeleteInventoryCommandHandler(IInventoryRepository inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task<Model.Inventory> Handle(DeleteInventoryCommand request, CancellationToken cancellationToken)
        {
            var inventory = await _inventoryService.GetByStorageDepotIdAndProductIdAsync(request.StoreDepotId, request.ProductId);
            if (inventory == null) {
                throw new Exception("Inventory not found or validate the id");  
            }   
            await _inventoryService.DeleteAsync(inventory);
            await _inventoryService.SaveChangesAsync();
            return inventory;
        }
    }
}
