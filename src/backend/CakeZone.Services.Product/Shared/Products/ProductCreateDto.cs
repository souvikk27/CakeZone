using CakeZone.Services.Product.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Product.Shared.Products
{
    public class ProductCreateDto
    {
        public ProductsDto Products { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public List<AttributeProductDto> AttributeProduct { get; set; }
    }
}