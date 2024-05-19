using CakeZone.Services.Product.Model;

namespace CakeZone.Services.Product.Services.Image
{
    public static class ImageHelper
    {
        public static async Task<List<ProductImage>> CreateProductImagesAsync(IImageService imageService, Guid productId, IFormFile mainImage, List<IFormFile> additionalImages)
        {
            var productImages = new List<ProductImage>();

            // Save the main image
            var mainImagePath = await imageService.SaveImageAsync(mainImage);
            productImages.Add(new ProductImage
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                Url = mainImagePath,
                IsMain = true,
                AltText = "Main Image",
                CreatedAt = DateTime.UtcNow
            });

            // Save additional images if any
            if (additionalImages != null && additionalImages.Any())
            {
                foreach (var image in additionalImages)
                {
                    var imagePath = await imageService.SaveImageAsync(image);
                    productImages.Add(new ProductImage
                    {
                        Id = Guid.NewGuid(),
                        ProductId = productId,
                        Url = imagePath,
                        IsMain = false,
                        AltText = "Additional Image",
                        CreatedAt = DateTime.UtcNow
                    });
                }
            }

            return productImages;
        }
    }
}
