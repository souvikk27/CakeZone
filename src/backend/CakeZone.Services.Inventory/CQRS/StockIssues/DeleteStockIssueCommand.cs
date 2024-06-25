using MediatR;

namespace CakeZone.Services.Inventory.CQRS.StockIssues
{
    public class DeleteStockIssueCommand : IRequest<Model.StockIssue>
    {
        public Guid Id { get; set; }

        public DeleteStockIssueCommand(Guid id)
        {
            Id = id;
        }
    }
}