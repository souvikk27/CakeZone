using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Product.Shared.Products
{
    public class ProductCreateDto
    {
        public ProductsDto Products { get; set; }

        [Required(ErrorMessage = "Please upload the main image.")]
        public string MainImageUrl { get; set; }

        public List<string>? AdditionalImageUrls { get; set; }
    }
}