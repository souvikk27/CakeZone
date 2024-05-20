using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Product.Shared.Products
{
    public class ProductCreateDto
    {
        public ProductsDto Products { get; set; }

        [Required(ErrorMessage = "Please upload the main image.")]
        public IFormFile MainImageUrl { get; set; }

        public List<IFormFile>? AdditionalImageUrls { get; set; }
    }
}