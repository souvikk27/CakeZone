using CakeZone.Services.Inventory.Shared.StockReceipt;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.StockReceipt
{
    public class UpdateStockReceiptCommand : IRequest<Model.StockReceipt>
    {
        public UpdateStockReceiptDto UpdateStockReceipt { get; set; }
        public UpdateStockReceiptCommand(UpdateStockReceiptDto updateStockReceipt)
        {
            UpdateStockReceipt = updateStockReceipt;
        }
    }
}
