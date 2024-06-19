using AutoMapper;
using CakeZone.Services.Inventory.Repository;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.Inventory
{
    public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, Model.Inventory>
    {
        private readonly IInventoryRepository _inventoryService;
        private readonly IMapper _mapper;

        public CreateInventoryCommandHandler(IInventoryRepository inventoryService, IMapper mapper)
        {
            _inventoryService = inventoryService;
            _mapper = mapper;
        }

        public async Task<Model.Inventory> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
        {
            var inventory = _mapper.Map<Model.Inventory>(request.CreateInventoryDto);
            await _inventoryService.AddAsync(inventory);
            await _inventoryService.SaveChangesAsync();
            return inventory;
        }
    }
}
