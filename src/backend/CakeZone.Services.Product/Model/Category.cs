using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Product.Model
{
    public partial class Category
    {
        [Key]
        public Guid CategoryId { get; set; }

        [Required]
        public string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }

        public virtual Category ParentCategory { get; set; }

        public virtual ICollection<Product> Products { get; set; }  
    }
}
