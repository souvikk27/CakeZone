using CakeZone.Services.Inventory.Repository.StockReceipt;
using MediatR;

namespace CakeZone.Services.Inventory.CQRS.StockReceipt;

public class DeleteStockReceiptCommandHandler : IRequestHandler<DeleteStockReceiptCommand, Model.StockReceipt>
{
    private readonly IStockReceiptRepository _stockReceiptRepository;

    public DeleteStockReceiptCommandHandler(IStockReceiptRepository stockReceiptRepository)
    {
        _stockReceiptRepository = stockReceiptRepository;
    }

    public async Task<Model.StockReceipt> Handle(DeleteStockReceiptCommand request, CancellationToken cancellationToken)
    {
        var stockReceipt = await _stockReceiptRepository.GetByIdAsync(request.Id);
        await _stockReceiptRepository.DeleteAsync(stockReceipt);
        await _stockReceiptRepository.SaveChangesAsync();
        return stockReceipt;
    }
}