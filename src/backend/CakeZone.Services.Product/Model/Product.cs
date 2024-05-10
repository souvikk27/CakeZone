using System.ComponentModel.DataAnnotations;
using CakeZone.Services.Product.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace CakeZone.Services.Product.Model
{
    public partial class Product
    {
        [Key] public Guid ProductId { get; set; }
        [Required] public string Name { get; set; }
        public string Description { get; set; }
        public Guid? CategoryId { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Attribute> Attributes { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }

}
