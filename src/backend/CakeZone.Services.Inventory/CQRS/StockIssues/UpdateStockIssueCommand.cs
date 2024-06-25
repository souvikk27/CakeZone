using CakeZone.Services.Inventory.Model;
using CakeZone.Services.Inventory.Shared.StockIssues;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.StockIssues
{
    public class UpdateStockIssueCommand : IRequest<StockIssue>
    {
        public UpdateStockIssueDto UpdateStockIssueDto { get; set; }
        public UpdateStockIssueCommand(UpdateStockIssueDto updateStockIssueDto)
        {
            UpdateStockIssueDto = updateStockIssueDto;
        }   
    }
}
