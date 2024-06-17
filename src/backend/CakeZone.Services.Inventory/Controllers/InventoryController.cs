using CakeZone.Services.Inventory.CQRS.Inventory;
using CakeZone.Services.Inventory.Services.Filters;
using Chronos.ApiResponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CakeZone.Services.Inventory.Controllers
{
    [Route("api/v1/inventory")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InventoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetInventory([FromQuery] InventoryParameter parameter)
        {
            var query = new GetInventoriesQuery(parameter);
            var inventories = await _mediator.Send(query);
            return ApiResponseExtension.ToPaginatedApiResult(inventories);
        }
    }
}
