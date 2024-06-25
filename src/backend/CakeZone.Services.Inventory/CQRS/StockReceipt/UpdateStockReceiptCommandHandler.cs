using AutoMapper;
using CakeZone.Services.Inventory.Model.Exceptions;
using CakeZone.Services.Inventory.Repository.StockReceipt;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.StockReceipt;

public class UpdateStockReceiptCommandHandler : IRequestHandler<UpdateStockReceiptCommand, Model.StockReceipt>
{
    private readonly IStockReceiptRepository _stockReceiptRepository;
    private readonly IMapper _mapper;

    public UpdateStockReceiptCommandHandler(IStockReceiptRepository stockReceiptRepository, IMapper mapper)
    {
        _stockReceiptRepository = stockReceiptRepository;
        _mapper = mapper;
    }

    public async Task<Model.StockReceipt> Handle(UpdateStockReceiptCommand request, CancellationToken cancellationToken)
    {
        var stockReceiptExists =
            await _stockReceiptRepository.CheckExistsAsync(x => x.Id == request.UpdateStockReceipt.Id);
        if (!stockReceiptExists)
        {
            throw new NotFoundApiException("stock receipt not found");
        }
        var stockReceipt = _mapper.Map<UpdateStockReceiptCommand, Model.StockReceipt>(request);
        await _stockReceiptRepository.UpdateAsync(stockReceipt);
        return stockReceipt;
    }
}