using CakeZone.Services.Inventory.Repository.Inv;
using MassTransit;
using SharedLibrary.Event;
using CakeZone.Services.Inventory.Model;
using CakeZone.Services.Inventory.Shared.Inventory;

namespace CakeZone.Services.Inventory.Event
{
    public class InventoryCreateConsumer : IConsumer<ProductCreated>
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryCreateConsumer(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task Consume(ConsumeContext<ProductCreated> context)
        {
            var inventory = new Model.Inventory()
            {
                ProductId = context.Message.ProductId,
                StorageDepotId = Guid.Parse("a015feaa-b072-4361-a056-7c6bf229b2a6"),
                CurrentLevel = 1000,
                MinLevel = 500,
                Demand = 1000,
                HoldingCostPerUnit = 120
            };
            await _inventoryRepository.AddAsync(inventory);
            await _inventoryRepository.SaveChangesAsync();
        }
    }
}
