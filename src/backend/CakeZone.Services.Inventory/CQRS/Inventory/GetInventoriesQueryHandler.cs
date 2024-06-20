using CakeZone.Services.Inventory.Repository.Inv;
using CakeZone.Services.Inventory.Services;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.Inventory
{
    public class GetInventoriesQueryHandler : IRequestHandler<GetInventoriesQuery, PagedList<Model.Inventory>>
    {
        private readonly IInventoryRepository _inventoryService;

        public GetInventoriesQueryHandler(IInventoryRepository inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task<PagedList<Model.Inventory>> Handle(GetInventoriesQuery request, CancellationToken cancellationToken)
        {
            var inventories = await _inventoryService.ListAllAsync();
            var metadata = new MetaData().Initialize(request.Parameter.PageNumber,
                request.Parameter.PageSize,
                inventories.Count());

            return PagedList<Model.Inventory>.ToPagedList(inventories.ToList(),
                request.Parameter.PageNumber,
                request.Parameter.PageSize);
        }
    }
}