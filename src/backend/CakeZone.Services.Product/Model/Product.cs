using System.ComponentModel.DataAnnotations;
namespace CakeZone.Services.Product.Model
{
    public sealed class Product
    {
        [Key] 
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Attribute> Attributes { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
    }

}
