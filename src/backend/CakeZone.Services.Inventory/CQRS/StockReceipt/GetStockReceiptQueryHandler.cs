using CakeZone.Services.Inventory.Repository.StockReceipt;
using CakeZone.Services.Inventory.Services;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.StockReceipt
{
    public class GetStockReceiptQueryHandler : IRequestHandler<GetStockReceiptQuery, PagedList<Model.StockReceipt>>
    {
        private readonly IStockReceiptRepository _stockReceiptRepository;

        public GetStockReceiptQueryHandler(IStockReceiptRepository stockReceiptRepository)
        {
            _stockReceiptRepository = stockReceiptRepository;
        }

        public async Task<PagedList<Model.StockReceipt>> Handle(GetStockReceiptQuery request, CancellationToken cancellationToken)
        {
            var stockReceipt = await _stockReceiptRepository.ListAllAsync();

            var filteredStockReceipt = stockReceipt.Where(x =>
                request.Parameter.ReceiptDate == DateTime.MinValue || request.Parameter.ReceiptDate == x.ReceiptDate);

            var metadata = new MetaData().Initialize(request.Parameter.PageNumber,
                request.Parameter.PageSize,
                filteredStockReceipt.Count());

            var pagedList = PagedList<Model.StockReceipt>.ToPagedList(filteredStockReceipt,
                request.Parameter.PageNumber,
                request.Parameter.PageSize);

            return pagedList;
        }
    }
}