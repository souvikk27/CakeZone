using CakeZone.Services.Inventory.Services;
using CakeZone.Services.Inventory.Services.Filters;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.Inventory
{
    public class GetInventoriesQuery : IRequest<PagedList<Model.Inventory>>
    {
        public InventoryParameter Parameter { get; set; }

        public GetInventoriesQuery(InventoryParameter parameter)
        {
            Parameter = parameter;
        }   
    }
}
