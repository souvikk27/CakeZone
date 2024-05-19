using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Product.Shared.Products
{
    public class ProductCreateDto
    {
        public ProductsDto Products { get; set; }
        [Required]
        public IFormFile MainImage { get; set; }
        public List<IFormFile> AdditionalImages { get; set; }
    }
}
