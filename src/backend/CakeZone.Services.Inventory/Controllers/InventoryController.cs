using CakeZone.Services.Inventory.CQRS.Inventory;
using CakeZone.Services.Inventory.Services.Filters;
using CakeZone.Services.Inventory.Services.Validation;
using CakeZone.Services.Inventory.Shared.Inventory;
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
            return ApiResponseExtension.ToPaginatedApiResult(inventories,
                "inventories", 
                "200", 
                inventories.MetaData.CurrentPage, 
                inventories.MetaData.TotalPages);
        }

        [HttpGet]
        [Route("product/{productId}/storage/{storageId}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetInventoryByProductIdAndStorageId([FromRoute] Guid productId, [FromRoute] Guid storageId)
        {
            var query = new GetInventoryByProductIdAndStorageIdQuery(productId, storageId);
            var inventory = await _mediator.Send(query);
            return ApiResponseExtension.ToSuccessApiResult(inventory);
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateInventory([FromBody] CreateInventoryDto createInventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var validator = new CreateInventoryValidator();
            var validationResult = await validator.ValidateAsync(createInventory);
            if (!validationResult.IsValid)
            {
                return ApiResponseExtension.ToErrorApiResult(validationResult.Errors.Select(e => e.ErrorMessage),"Validation error"
                    ,"400");
            }
            var command = new CreateInventoryCommand(createInventory);
            var inventory = await _mediator.Send(command);
            return ApiResponseExtension.ToSuccessApiResult(inventory);
        }

        [HttpPut]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateInventory([FromBody] UpdateInventoryDto updateInventory)
        {
            var validator = new UpdateInventoryValidator();
            var validationResult = await validator.ValidateAsync(updateInventory);
            if (!validationResult.IsValid)
            {
                return ApiResponseExtension.ToErrorApiResult(validationResult.Errors.Select(e => e.ErrorMessage),"Validation error"
                    ,"400");
            }
            var command = new UpdateInventoryCommand(updateInventory);
            var inventory = await _mediator.Send(command);
            return ApiResponseExtension.ToSuccessApiResult(inventory);
        }


        [HttpDelete]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteInventory([FromQuery] Guid productIdGuid, [FromQuery] Guid storageGuid)
        {
            var command = new DeleteInventoryCommand(productIdGuid, storageGuid);
            var inventory = await _mediator.Send(command);
            return ApiResponseExtension.ToSuccessApiResult(inventory);
        }
    }
}
