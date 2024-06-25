using AutoMapper;
using CakeZone.Services.Inventory.Repository.StockReceipt;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.StockReceipt
{
    public class CreateStockReceiptCommandHandler : IRequestHandler<CreateStockReceiptCommand, Model.StockReceipt>
    {
        private readonly IStockReceiptRepository _stockReceiptRepository;
        private readonly IMapper _mapper;

        public CreateStockReceiptCommandHandler(IStockReceiptRepository stockReceiptRepository, IMapper mapper)
        {
            _stockReceiptRepository = stockReceiptRepository;
            _mapper = mapper;
        }

        public async Task<Model.StockReceipt> Handle(CreateStockReceiptCommand request, CancellationToken cancellationToken)
        {
            var stockReceipt = _mapper.Map<Model.StockReceipt>(request.CreateStockReceiptDto);
            await _stockReceiptRepository.AddAsync(stockReceipt);
            await _stockReceiptRepository.SaveChangesAsync();

            return stockReceipt;
        }
    }
}
