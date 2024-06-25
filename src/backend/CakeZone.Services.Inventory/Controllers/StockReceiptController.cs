using CakeZone.Services.Inventory.CQRS.StockReceipt;
using CakeZone.Services.Inventory.Services.Filters;
using CakeZone.Services.Inventory.Shared.StockReceipt;
using Chronos.ApiResponse;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace CakeZone.Services.Inventory.Controllers
{
    [Route("api/v1/stockreceipts")]
    [ApiController]
    public class StockReceiptController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StockReceiptController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetStockReceipts([FromQuery] StockReceiptParameter parameter)
        {
            var query = new GetStockReceiptQuery(parameter);
            var stockReceipts = await _mediator.Send(query);
            return ApiResponseExtension.ToPaginatedApiResult(stockReceipts,
                "stock receipts",
                "200",
                stockReceipts.MetaData.CurrentPage,
                stockReceipts.MetaData.TotalPages);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStockReceipt([FromBody] CreateStockReceiptDto createStockReceipt)
        {
            var command = new CreateStockReceiptCommand(createStockReceipt);
            var stockReceipt = await _mediator.Send(command);
            return ApiResponseExtension.ToSuccessApiResult(stockReceipt);
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateStockReceipt([FromBody] UpdateStockReceiptDto updateStockReceipt)
        {
            var command = new UpdateStockReceiptCommand(updateStockReceipt);
            var stockReceipt = await _mediator.Send(command);
            return ApiResponseExtension.ToSuccessApiResult(stockReceipt);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockReceipt(Guid id)
        {
            var command = new DeleteStockReceiptCommand(id);
            var stockReceipt = await _mediator.Send(command);
            return ApiResponseExtension.ToSuccessApiResult(stockReceipt);
        }
    }
}
