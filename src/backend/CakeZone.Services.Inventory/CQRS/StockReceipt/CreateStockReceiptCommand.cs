using CakeZone.Services.Inventory.Shared.StockIssues;
using CakeZone.Services.Inventory.Shared.StockReceipt;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.StockReceipt
{
    public class CreateStockReceiptCommand : IRequest<Model.StockReceipt>
    {
        public CreateStockReceiptDto CreateStockReceiptDto { get; set; }

        public CreateStockReceiptCommand(CreateStockReceiptDto createStockIssueDto)
        {
            CreateStockReceiptDto = createStockIssueDto;
        }   
    }
}
