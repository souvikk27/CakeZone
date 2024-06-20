using CakeZone.Services.Inventory.CQRS.Depot;
using CakeZone.Services.Inventory.Services.Filters;
using CakeZone.Services.Inventory.Shared.Depot;
using Chronos.ApiResponse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CakeZone.Services.Inventory.Controllers
{
    [Route("api/v1/storage")]
    [ApiController]
    public class StorageDepotController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StorageDepotController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetStorageDepot([FromQuery] DepotParameter parameter)
        {
            var query = new GetStorageDepotsQuery(parameter);
            var depots = await _mediator.Send(query);
            return ApiResponseExtension.ToPaginatedApiResult(depots);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStorageDepot([FromBody] CreateStorageDepotDto createStorageDepot)
        {
            var command = new CreateStorageDepotCommand(createStorageDepot);
            var storageDepot = await _mediator.Send(command);
            return ApiResponseExtension.ToSuccessApiResult(storageDepot);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStorageDepot([FromBody] UpdateStorageDepotDto updateStorageDepotDto)
        {
            var command = new UpdateStorageDepotCommand(updateStorageDepotDto);
            var storageDepot = await _mediator.Send(command);
            return ApiResponseExtension.ToSuccessApiResult(storageDepot);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStorageDepot(Guid depotId)
        {
            var command = new DeleteStorageDepotCommand(depotId);
            var storageDepot = await _mediator.Send(command);
            return ApiResponseExtension.ToSuccessApiResult(storageDepot);
        }
    }
}
