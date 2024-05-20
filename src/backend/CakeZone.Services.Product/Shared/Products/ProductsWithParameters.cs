using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Product.Shared.Products
{
    public class ProductsWithParameters
    {
        public ProductsDto Products { get; set; }

        public Guid CategoryId { get; set; }

        public List<Guid> AttributeIds { get; set; }

        [Required(ErrorMessage = "Please upload the main image.")]
        public IFormFile MainImageUrl { get; set; }

        public List<IFormFile>? AdditionalImageUrls { get; set; }
    }
}
