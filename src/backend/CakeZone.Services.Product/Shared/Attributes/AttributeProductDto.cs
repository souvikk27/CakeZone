using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Product.Shared.Attributes
{
    public class AttributeProductDto
    {
        [Required]
        public Guid AttributeId { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
