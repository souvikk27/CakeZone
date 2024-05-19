using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Product.Shared.Products
{
    public record ProductsDto
    {
        public Guid ProductId = Guid.NewGuid();

        [Required(ErrorMessage = "Product name is required!")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Product name must be between 5 and 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "SKU is required!")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "SKU must be between 5 and 20 characters")]
        public string Sku { get; set; }

        [Required(ErrorMessage = "Product description is required!")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Product description must be between 10 and 500 characters")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price is required!")]
        public decimal Price { get; set; }

        public DateTime CreatedAt = DateTime.Now;

        public bool IsDeleted = false;
    }
}
