using CakeZone.Services.Inventory.Model;
using CakeZone.Services.Inventory.Repository.StockIssue;
using CakeZone.Services.Inventory.Services;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.StockIssues
{
    public class GetStockIssuesQueryHandler: IRequestHandler<GetStockIssuesQuery, PagedList<StockIssue>>
    {
        private readonly IStockIssuesRepository _stockIssuesRepository;

        public GetStockIssuesQueryHandler(IStockIssuesRepository stockIssuesRepository)
        {
            _stockIssuesRepository = stockIssuesRepository;
        }

        public async Task<PagedList<StockIssue>> Handle(GetStockIssuesQuery request, CancellationToken cancellationToken)
        {
            var stockIssues = await _stockIssuesRepository.ListAllAsync();

            var filteredStockIssues = stockIssues.Where(r =>
                request.Parameter.IssueDate == DateTime.MinValue || request.Parameter.IssueDate == r.IssueDate);

            var metaData = new MetaData().Initialize(request.Parameter.PageNumber, request.Parameter.PageSize,
                filteredStockIssues.Count());

            var pagedList = PagedList<StockIssue>.ToPagedList(filteredStockIssues,
                request.Parameter.PageNumber,
                request.Parameter.PageSize);

            return pagedList;
        }
    }
}
