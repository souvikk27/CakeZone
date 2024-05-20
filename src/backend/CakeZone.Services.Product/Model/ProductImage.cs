using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Product.Model
{
    public partial class ProductImage
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string AltText { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
