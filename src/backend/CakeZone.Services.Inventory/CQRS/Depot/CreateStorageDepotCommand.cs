using CakeZone.Services.Inventory.Shared.Depot;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.Depot
{
    public class CreateStorageDepotCommand : IRequest<Model.StorageDepot>
    {
        public CreateStorageDepotDto CreateStorageDepotDto { get; set; }

        public CreateStorageDepotCommand(CreateStorageDepotDto createStorageDepotDto)
        {
            CreateStorageDepotDto = createStorageDepotDto;
        }
    }
}
