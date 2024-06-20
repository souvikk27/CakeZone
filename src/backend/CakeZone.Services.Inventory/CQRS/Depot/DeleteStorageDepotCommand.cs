using CakeZone.Services.Inventory.Model;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.Depot;

public class DeleteStorageDepotCommand : IRequest<StorageDepot>
{
    public Guid Id { get; set; }
    public DeleteStorageDepotCommand(Guid depotId)
    {
        Id = depotId;
    }
}