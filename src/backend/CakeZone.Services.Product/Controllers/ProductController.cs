using AutoMapper;
using CakeZone.Services.Product.Extension;
using CakeZone.Services.Product.Repository.Image;
using CakeZone.Services.Product.Repository.Product;
using CakeZone.Services.Product.Services;
using CakeZone.Services.Product.Services.FIlters;
using CakeZone.Services.Product.Services.Image;
using CakeZone.Services.Product.Services.Logging;
using CakeZone.Services.Product.Shared.Products;
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
        private readonly IImageService _imageService;
        private readonly IProductImageRepository _productImageRepository;

        /// <inheritdoc />
        public ProductController(ILoggerManager logger,
            IProductRepository productRepository,
            IMapper mapper,
            IImageService imageService,
            IProductImageRepository productImageRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            _mapper = mapper;
            _imageService = imageService;
            _productImageRepository = productImageRepository;
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
            var products = await _productRepository.GetAll();
            IEnumerable<ProductViewDto> productsView = _mapper.Map<IEnumerable<ProductViewDto>>(products);
            var filteredProduct = productsView.Where(product =>
                                     (productParameter.AddedOn == DateTime.MinValue || productParameter.AddedOn == product.CreatedAt) &&
                                     (string.IsNullOrEmpty(productParameter.ProductName) || productParameter.ProductName == product.Name))
                                     .ToList();
            var metadata = new MetaData().Initialize(productParameter.PageNumber, productParameter.PageSize, filteredProduct.Count());
            metadata.AddResponseHeaders(Response);
            var pagedList = PagedList<ProductViewDto>.ToPagedList(filteredProduct, productParameter.PageNumber, productParameter.PageSize);
            return Ok(pagedList);
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
            var productExist = await _productRepository.FindAsync(p => p.Name == productCreateDto.Products.Name);

            if (productExist.Any())
            {
                return ApiResponseExtension.ToWarningApiResult("Bad Request", "Requested product with name already exists!", "400");
            }
            var product = _mapper.Map<Model.Product>(productCreateDto.Products);
            await _productRepository.AddProductsWithParametersAsync(product, productCreateDto.CategoryId, productCreateDto.AttributeProduct);
            await _productRepository.SaveAsync();
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
            var productImage = await _productImageRepository.FindAsync(p => p.ProductId == id);
            
            var location = productImage.FirstOrDefault().Url;

            var result = await _imageService.RemoveImageAsync(location);

            if (result)
            {
                _logger.LogInfo("Image found and removed from product");
            }
            await _productImageRepository.Remove(productImage.FirstOrDefault());
            await _productImageRepository.SaveAsync();
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