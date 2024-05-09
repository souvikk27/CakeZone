using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Product.Model
{
    public class Attribute
    {
        [Key]
        public Guid AttributeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
