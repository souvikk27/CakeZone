using MediatR;

namespace CakeZone.Services.Inventory.CQRS.StockReceipt
{
    public class DeleteStockReceiptCommand : IRequest<Model.StockReceipt>
    {
        public Guid Id { get; set; }
        public DeleteStockReceiptCommand(Guid id)
        {
            Id = id;
        }
    }
}
