using CakeZone.Services.Inventory.Model;
using CakeZone.Services.Inventory.Services;
using CakeZone.Services.Inventory.Services.Filters;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.StockIssues;

public class GetStockIssuesQuery : IRequest<PagedList<StockIssue>>
{
    public StockIssueParameter Parameter { get; set; }

    public GetStockIssuesQuery(StockIssueParameter parameter)
    {
        Parameter = parameter;
    }
}