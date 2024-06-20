using CakeZone.Services.Inventory.Shared.Depot;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.Depot;

public class UpdateStorageDepotCommand : IRequest<Model.StorageDepot>
{
    public UpdateStorageDepotDto UpdateStorageDepotDto { get; set; }
    public UpdateStorageDepotCommand(UpdateStorageDepotDto updateStorageDepotDto)
    {
        UpdateStorageDepotDto = updateStorageDepotDto;
    }
}