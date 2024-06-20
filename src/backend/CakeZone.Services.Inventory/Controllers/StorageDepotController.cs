using CakeZone.Services.Inventory.CQRS.Depot;
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

        [HttpPost]
        public async Task<IActionResult> CreateStorageDepot([FromBody] CreateStorageDepotDto createStorageDepot)
        {
            var command = new CreateStorageDepotCommand(createStorageDepot);
            var storageDepot = await _mediator.Send(command);
            return ApiResponseExtension.ToSuccessApiResult(storageDepot);
        }
    }
}
