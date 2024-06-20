using CakeZone.Services.Inventory.Shared.Inventory;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.Inventory
{
    public class UpdateInventoryCommand : IRequest<Model.Inventory>

    {
    public UpdateInventoryDto UpdateInventoryDto { get; set; }

    public UpdateInventoryCommand(UpdateInventoryDto updateInventoryDto)
    {
        UpdateInventoryDto = updateInventoryDto;
    }
    }
}
