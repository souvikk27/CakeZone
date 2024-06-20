using MediatR;

namespace CakeZone.Services.Inventory.CQRS.Inventory
{
    public class GetInventoryByProductIdAndStorageIdQuery : IRequest<Model.Inventory>
    {
        public Guid ProductId { get; set; }
        public Guid StoreDepotId { get; set; }

        public GetInventoryByProductIdAndStorageIdQuery(Guid productId, Guid storeDepotId)
        {
            ProductId = productId;
            StoreDepotId = storeDepotId;
        }
    }
}