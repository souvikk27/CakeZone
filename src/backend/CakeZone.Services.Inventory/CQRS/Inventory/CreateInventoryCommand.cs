using CakeZone.Services.Inventory.Shared.Inventory;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.Inventory
{
    public class CreateInventoryCommand : IRequest<Model.Inventory>
    {
        public CreateInventoryDto CreateInventoryDto { get; set; }

        public CreateInventoryCommand(CreateInventoryDto createInventoryDto)
        {
            CreateInventoryDto = createInventoryDto;
        }
    }
}
