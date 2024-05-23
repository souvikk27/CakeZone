using CakeZone.Services.Product.Extension;
using CakeZone.Services.Product.Repository.Image;
using CakeZone.Services.Product.Services.Image;
using CakeZone.Services.Product.Shared.Images;
using Microsoft.AspNetCore.Mvc;

namespace CakeZone.Services.Product.Controllers
{
    [ApiController]
    [Route("api/v1/images")]
    public class ImageController : ControllerBase
    {
        private readonly IProductImageRepository _productImageRepository;
        private readonly IImageService _imageService;

        public ImageController(IProductImageRepository productImageRepository, IImageService imageService)
        {
            _productImageRepository = productImageRepository;
            _imageService = imageService;
        }

        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        [Produces("application/json")]
        public async Task<IActionResult> UploadProductImages([FromForm] ProductImageCreateDto productImageDto)
        {
            var productImages = await ImageHelper.CreateProductImagesAsync(_imageService,
                productImageDto.ProductId,
                productImageDto.MainImageUrl,
                productImageDto.AdditionalImageUrls);

            foreach (var productImage in productImages)
            {
                await _productImageRepository.AddAsync(productImage);
            }
            await _productImageRepository.SaveAsync();
            return ApiResponseExtension.ToSuccessApiResult(productImages, "product images created", "200");
        }

        [HttpDelete]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteProductImages([FromQuery] Guid productId)
        {
            var productImage = await _productImageRepository.FindAsync(p => p.ProductId == productId);

            foreach (var image in productImage)
            {
                var location = image.Url;
                var result = await _imageService.RemoveImageAsync(location);
                await _productImageRepository.Remove(image);
                await _productImageRepository.SaveAsync();
            }
            return ApiResponseExtension.ToSuccessApiResult("Removed", "product images removed", "200");
        }
    }
}