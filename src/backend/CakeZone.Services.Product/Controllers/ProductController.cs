using AutoMapper;
using CakeZone.Services.Product.Extension;
using CakeZone.Services.Product.Repository.Image;
using CakeZone.Services.Product.Repository.Product;
using CakeZone.Services.Product.Services.Image;
using CakeZone.Services.Product.Services.Logging;
using CakeZone.Services.Product.Shared.Products;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

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
                return ApiResponseExtension.ToErrorApiResult("Not Found", $"Product with {productName} not found", "404");
            }
            return ApiResponseExtension.ToSuccessApiResult(product, "Product");
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreateDto productCreateDto)
        {
            if (productCreateDto.Products == null || productCreateDto.MainImageUrl == null)
            {
                return ApiResponseExtension.ToErrorApiResult("Bad Request", "Product data or main image file is missing.", "400");
            }

            var productExist = await _productRepository.FindAsync(p => p.Name == productCreateDto.Products.Name);

            if (productExist.Any())
            {
                return ApiResponseExtension.ToWarningApiResult("Bad Request", "Requested product with name already exists!", "400");
            }
            var product = _mapper.Map<Model.Product>(productCreateDto.Products);
            await _productRepository.AddAsync(product);
            await _productRepository.SaveAsync();
            var productImages = await ImageHelper.CreateProductImagesAsync(_imageService,
                product.Id,
                productCreateDto.MainImageUrl,
                productCreateDto.AdditionalImageUrls);

            foreach (var productImage in productImages)
            {
                await _productImageRepository.AddAsync(productImage);
            }
            await _productImageRepository.SaveAsync();
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