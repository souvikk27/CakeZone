using CakeZone.Services.Inventory.CQRS.StockIssues;
using CakeZone.Services.Inventory.Services.Filters;
using CakeZone.Services.Inventory.Shared.StockIssues;
using Chronos.ApiResponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CakeZone.Services.Inventory.Controllers
{
    [Route("api/v1/stockissues")]
    [ApiController]
    public class StockIssuesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StockIssuesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetStockIssues([FromQuery] StockIssueParameter parameter)
        {
            var query = new GetStockIssuesQuery(parameter);
            var stockIssues = await _mediator.Send(query);
            return ApiResponseExtension.ToPaginatedApiResult(stockIssues,
                "stock issues",
                "200",
                stockIssues.MetaData.CurrentPage,
                stockIssues.MetaData.TotalPages);
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateStockIssue([FromBody] CreateStockIssueDto createStockReceipt)
        {
            var command = new CreateStockIssueCommand(createStockReceipt);
            var stockIssue = await _mediator.Send(command);
            return ApiResponseExtension.ToSuccessApiResult(stockIssue);
        }

        [HttpPut]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateStockIssue([FromBody] UpdateStockIssueDto updateStockIssue)
        {
            var command = new UpdateStockIssueCommand(updateStockIssue);
            var stockIssue = await _mediator.Send(command);
            return ApiResponseExtension.ToSuccessApiResult(stockIssue);
        }

        [HttpDelete("{stockIssueId}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteStockIssue([FromQuery] Guid stockIssueId)
        {
            var command = new DeleteStockIssueCommand(stockIssueId);
            var stockIssue = await _mediator.Send(command);
            return ApiResponseExtension.ToSuccessApiResult(stockIssue);
        }
    }
}