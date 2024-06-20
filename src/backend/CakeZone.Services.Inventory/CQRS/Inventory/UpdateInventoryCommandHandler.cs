using AutoMapper;
using CakeZone.Services.Inventory.Repository.Inv;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.Inventory
{
    public class UpdateInventoryCommandHandler : IRequestHandler<UpdateInventoryCommand, Model.Inventory>
    {
        private readonly IInventoryRepository _inventoryService;
        private readonly IMapper _mapper;

        public UpdateInventoryCommandHandler(IInventoryRepository inventoryService, IMapper mapper)
        {
            _inventoryService = inventoryService;
            _mapper = mapper;
        }

        public async Task<Model.Inventory> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
        {
            var inventoyExists = await _inventoryService.GetByStorageDepotIdAndProductIdAsync(request.UpdateInventoryDto.StorageDepotId, request.UpdateInventoryDto.ProductId);

            if (inventoyExists == null)
            {
                throw new Exception("Inventory not found or validate the id");
            }

            var inventory = _mapper.Map<Model.Inventory>(request.UpdateInventoryDto);
            await _inventoryService.UpdateAsync(inventory);
            await _inventoryService.SaveChangesAsync();
            return inventory;
        }
    }
}
