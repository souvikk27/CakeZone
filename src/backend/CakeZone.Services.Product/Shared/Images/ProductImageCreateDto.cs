using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Product.Shared.Images
{
    public class ProductImageCreateDto
    {
        [Required(ErrorMessage = "Please enter product id.")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Please upload the main image.")]
        public IFormFile MainImageUrl { get; set; }

        public List<IFormFile>? AdditionalImageUrls { get; set; }
    }
}
