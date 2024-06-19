using System.ComponentModel.DataAnnotations;
using CakeZone.Services.Product.Extension;

namespace CakeZone.Services.Product.Model
{
    public partial class Category
    {
        [Key] public Guid Id { get; set; }
        
        public string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public virtual Category ParentCategory { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
