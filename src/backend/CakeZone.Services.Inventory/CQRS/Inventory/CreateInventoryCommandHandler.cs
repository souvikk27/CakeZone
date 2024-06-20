using AutoMapper;
using CakeZone.Services.Inventory.Repository.Depot;
using CakeZone.Services.Inventory.Repository.Inv;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.Inventory
{
    public class CreateInventoryCommandHandler : IRequestHandler<CreateInventoryCommand, Model.Inventory>
    {
        private readonly IInventoryRepository _inventoryService;
        private readonly IMapper _mapper;
        private readonly IStorageDepotRepository _storageDepotRepository;

        public CreateInventoryCommandHandler(IInventoryRepository inventoryService, IMapper mapper,
            IStorageDepotRepository storageDepotRepository)
        {
            _inventoryService = inventoryService;
            _mapper = mapper;
            _storageDepotRepository = storageDepotRepository;
        }

        public async Task<Model.Inventory> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
        {
            var storageDepot = await _storageDepotRepository.GetByIdAsync(request.CreateInventoryDto.StorageDepotId);
            if (storageDepot == null)
            {
                throw new Exception("Storage depot not found either create it first or validate the id");
            }

            var inventory = _mapper.Map<Model.Inventory>(request.CreateInventoryDto);
            await _inventoryService.AddAsync(inventory);
            await _inventoryService.SaveChangesAsync();
            return inventory;
        }
    }
}
