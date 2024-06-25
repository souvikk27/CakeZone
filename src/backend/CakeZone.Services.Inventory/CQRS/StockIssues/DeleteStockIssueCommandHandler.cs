using CakeZone.Services.Inventory.Model;
using CakeZone.Services.Inventory.Model.Exceptions;
using CakeZone.Services.Inventory.Repository.StockIssue;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.StockIssues
{
    public class DeleteStockIssueCommandHandler : IRequestHandler<DeleteStockIssueCommand, Model.StockIssue>
    {
        private readonly IStockIssuesRepository _stockIssuesRepository;

        public DeleteStockIssueCommandHandler(IStockIssuesRepository stockIssuesRepository)
        {
            _stockIssuesRepository = stockIssuesRepository;
        }

        public async Task<StockIssue> Handle(DeleteStockIssueCommand request, CancellationToken cancellationToken)
        {
            var stockIssues = await _stockIssuesRepository.GetByIdAsync(request.Id);
            if (stockIssues == null)
            {
                throw new NotFoundApiException(
                    "Requested stock issue not found either validate parameters or contact support");
            }

            await _stockIssuesRepository.DeleteAsync(stockIssues);
            await _stockIssuesRepository.SaveChangesAsync();

            return stockIssues;
        }
    }
}