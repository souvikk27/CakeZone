using CakeZone.Services.Inventory.Services;
using CakeZone.Services.Inventory.Services.Filters;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.StockReceipt
{
    public class GetStockReceiptQuery : IRequest<PagedList<Model.StockReceipt>>
    {
        public StockReceiptParameter Parameter { get; set; }

        public GetStockReceiptQuery(StockReceiptParameter parameter)
        {
            Parameter = parameter;
        }   
    }
}
