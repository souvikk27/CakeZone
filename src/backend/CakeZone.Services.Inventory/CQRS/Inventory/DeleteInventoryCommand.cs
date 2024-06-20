using MediatR;

namespace CakeZone.Services.Inventory.CQRS.Inventory
{
    public class DeleteInventoryCommand : IRequest<Model.Inventory>
    {
        public Guid ProductId { get; set; }
        public Guid StoreDepotId { get; set; }

        public DeleteInventoryCommand(Guid productId, Guid storeDepotId)
        {
            ProductId = productId;
            StoreDepotId = storeDepotId;
        }
    }
}
