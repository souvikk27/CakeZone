using CakeZone.Services.Inventory.CQRS.Inventory;
using CakeZone.Services.Inventory.Event;
using CakeZone.Services.Inventory.Services.Filters;
using Chronos.ApiResponse;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CakeZone.Services.Inventory.Controllers
{
    [Route("api/v1/inventory")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;

        public InventoryController(IMediator mediator, IPublishEndpoint publishEndpoint)
        {
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
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
