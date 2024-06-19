using CakeZone.Services.Inventory.Repository;
using MassTransit;
using SharedLibrary.Event;

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
            var inventory = new Model.Inventory
            {
                ProductId = context.Message.ProductId,
                StorageDepotId = context.Message.StorageDepotId,
                MaxLevel = context.Message.MaxLevel,
                CurrentLevel = context.Message.CurrentLevel,
                MinLevel = context.Message.MinLevel,
                AverageDemand = 0,
                StandardDeviationDemand = 0,
                Demand = 0,
                LeadTime = 0,
                HoldingCostPerUnit = 0,
                ShortageCostPerUnit = 0,
                InventoryPosition = 2
            };
            await _inventoryRepository.AddAsync(inventory);
            await _inventoryRepository.SaveChangesAsync();
        }
    }
}
