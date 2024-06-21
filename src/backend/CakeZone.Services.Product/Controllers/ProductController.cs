using CakeZone.Services.Product.CQRS.Product;
using CakeZone.Services.Product.Extension;
using CakeZone.Services.Product.Services.Filters;
using CakeZone.Services.Product.Services.Logging;
using CakeZone.Services.Product.Shared.Products;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Event;

namespace CakeZone.Services.Product.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;

        /// <inheritdoc />
        public ProductController(ILoggerManager logger,
            IMediator mediator, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductParameter productParameter)
        {
            var query = new GetAllProductsQuery(productParameter);
            var pagedList = await _mediator.Send(query);
            return ApiResponseExtension.ToPaginatedApiResult(pagedList,
                "products",
                "200",
                pagedList.MetaData.CurrentPage,
                pagedList.MetaData.TotalPages);
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("product")]
        public async Task<IActionResult> GetProductByName([FromQuery] string productName)
        {
            var query = new GetProductByNameQuery(productName);
            var product = await _mediator.Send(query);
            if (product == null)
            {
                return ApiResponseExtension.ToErrorApiResult("Not Found",
                    $"Product with name {productName} not found",
                    "404");
            }
            return ApiResponseExtension.ToSuccessApiResult(product, "Product");
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("product/sku")]
        public async Task<IActionResult> GetProductBySku([FromQuery] string sku)
        {
            var query = new GetProductsBySkuQuery(sku);
            var product = await _mediator.Send(query);
            return product == null
                ? ApiResponseExtension.ToErrorApiResult("Not Found",
                    $"Product with sku {sku} not found",
                    "404")
                : ApiResponseExtension.ToSuccessApiResult(product,
                    "Product");
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto productCreateDto)
        {
            
            var command = new CreateProductCommand(productCreateDto);
            var product = await _mediator.Send(command);
            var productCreated = new ProductCreated
            {
                ProductId = productCreateDto.Products.Id,
                StorageDepotId = Guid.NewGuid(),
                MaxLevel = 1000,
                CurrentLevel = 1000,
                MinLevel = 500
            };
            await _publishEndpoint.Publish(productCreated);
            return ApiResponseExtension.ToSuccessApiResult(product, "Product created", "200");
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var command = new DeleteProductCommand(id);
            var product = await _mediator.Send(command);
            return ApiResponseExtension.ToSuccessApiResult(product, "product removed", "200");
        }

        [HttpPut]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductsUpdateDto productsDto)
        {
            var command = new UpdateProductCommand(productsDto);
            var product = await _mediator.Send(command);
            return ApiResponseExtension.ToSuccessApiResult(product, "product updated", "204");
        }
    }
}