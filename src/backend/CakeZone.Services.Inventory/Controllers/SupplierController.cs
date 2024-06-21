using CakeZone.Services.Inventory.CQRS.Supplier;
using CakeZone.Services.Inventory.Services.Filters;
using CakeZone.Services.Inventory.Shared.Supplier;
using Chronos.ApiResponse;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace CakeZone.Services.Inventory.Controllers
{
    [Route("api/v1/suppliers")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SupplierController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetSuppliers([FromQuery] SupplierParameter parameter)
        {
            var query = new GetSuppliersQuery(parameter);
            var suppliers = await _mediator.Send(query);
            return ApiResponseExtension.ToPaginatedApiResult(suppliers,
                "suppliers",
                "200",
                suppliers.MetaData.CurrentPage,
                suppliers.MetaData.TotalPages);
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateSupplier([FromBody] CreateSupplierDto createSupplier)
        {
            var command = new CreateSupplierCommand(createSupplier);
            var supplier = await _mediator.Send(command);
            return ApiResponseExtension.ToSuccessApiResult(supplier);
        }
        
        [HttpPut]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateSupplier([FromBody] UpdateSupplierDto updateSupplier)
        {
            var command = new UpdateSupplierCommand(updateSupplier);
            var supplier = await _mediator.Send(command);
            return ApiResponseExtension.ToSuccessApiResult(supplier);
        }
        
        [HttpDelete("{supplierId}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteSupplier(Guid supplierId)
        {
            var command = new DeleteSupplierCommand(supplierId);
            var supplier = await _mediator.Send(command);
            return ApiResponseExtension.ToSuccessApiResult(supplier);
        }
    }
}
