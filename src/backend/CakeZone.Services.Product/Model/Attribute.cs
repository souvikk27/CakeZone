using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Product.Model
{
    public class Attribute
    {
        [Key]
        public Guid AttributeId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public Guid? ParentAttributeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
