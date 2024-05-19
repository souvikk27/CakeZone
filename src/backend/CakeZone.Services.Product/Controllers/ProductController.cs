using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using CakeZone.Services.Product.Extension;
using CakeZone.Services.Product.Repository.Image;
using CakeZone.Services.Product.Repository.Product;
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

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SuppressMessage("ReSharper.DPA", "DPA0011: High execution time of MVC action", MessageId = "time: 876ms")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto productCreateDto)
        {
            if (productCreateDto.Products == null || productCreateDto.MainImage == null)
            {
                return ApiResponseExtension.ToErrorApiResult("Product data or main image file is missing.", "Bad Request", "400");
            }

            var product = _mapper.Map<Model.Product>(productCreateDto.Products);

            await _productRepository.AddAsync(product);
            await _productRepository.SaveAsync();

            var productImages = await ImageHelper.CreateProductImagesAsync(_imageService,
                product.ProductId,
                productCreateDto.MainImage,
                productCreateDto.AdditionalImages);

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
        [SuppressMessage("ReSharper.DPA", "DPA0011: High execution time of MVC action", MessageId = "time: 517ms")]
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
