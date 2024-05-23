using AutoMapper;
using CakeZone.Services.Product.CQRS.Product;
using CakeZone.Services.Product.Extension;
using CakeZone.Services.Product.Repository.Product;
using CakeZone.Services.Product.Services.FIlters;
using CakeZone.Services.Product.Services.Logging;
using CakeZone.Services.Product.Shared.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CakeZone.Services.Product.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        /// <inheritdoc />
        public ProductController(ILoggerManager logger,
            IProductRepository productRepository,
            IMapper mapper,
            IMediator mediator)
        {
            _logger = logger;
            _productRepository = productRepository;
            _mapper = mapper;
            _mediator = mediator;
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
            return ApiResponseExtension.ToPaginatedApiResult(pagedList, "products", "200", pagedList.MetaData.CurrentPage, pagedList.MetaData.TotalPages);
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
            var product = await _productRepository.FindAsync(p => p.Name.Equals(productName));
            if (!product.Any())
            {
                return ApiResponseExtension.ToErrorApiResult("Not Found", $"Product with name {productName} not found", "404");
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
            var product = await _productRepository.GetProductsWithImages(sku);
            if (!product.Any())
            {
                return ApiResponseExtension.ToErrorApiResult("Not Found", $"Product with sku {sku} not found", "404");
            }
            return ApiResponseExtension.ToSuccessApiResult(product, "Product");
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
            if(product != null)
            {
                return ApiResponseExtension.ToErrorApiResult("Bad Request", 
                    $"Product with name {productCreateDto.Products.Name} " +
                    $"already exists eiher change product name or contact support!", "400");
            }
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
            var product = await _productRepository.GetById(id);

            await _productRepository.Remove(product);
            await _productRepository.SaveAsync();
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
            var products = _mapper.Map<Model.Product>(productsDto);
            await _productRepository.UpdateAsync(products);
            await _productRepository.SaveAsync();
            return ApiResponseExtension.ToSuccessApiResult(products, "product updated", "204");
        }
    }
}