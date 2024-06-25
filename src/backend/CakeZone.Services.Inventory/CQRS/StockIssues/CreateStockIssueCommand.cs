using CakeZone.Services.Inventory.Model;
using CakeZone.Services.Inventory.Shared.StockIssues;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.StockIssues
{
    public class CreateStockIssueCommand : IRequest<StockIssue>
    {
        public CreateStockIssueDto CreateStockReceiptDto { get; set; }

        public CreateStockIssueCommand(CreateStockIssueDto createStockReceiptDto)
        {
            CreateStockReceiptDto = createStockReceiptDto;
        }
    }
}
