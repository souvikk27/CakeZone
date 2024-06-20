using CakeZone.Services.Inventory.Model;
using CakeZone.Services.Inventory.Services.Filters;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.Depot;

public class GetStorageDepotsQuery : IRequest<IEnumerable<StorageDepot>>
{
    public DepotParameter Parameter { get; set; }
    public GetStorageDepotsQuery(DepotParameter parameter)
    {
        Parameter = parameter;
    }
}