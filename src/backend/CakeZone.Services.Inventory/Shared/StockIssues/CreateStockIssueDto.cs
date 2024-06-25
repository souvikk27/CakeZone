using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Inventory.Shared.StockIssues
{
    public class CreateStockIssueDto
    {
        public Guid Id = Guid.NewGuid();

        [Required(ErrorMessage = "Product Id is required")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Storage Depot Id is required")]
        public Guid StorageDepotId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Issue Date is required")]
        public DateTime IssueDate { get; set; }

        public DateTime CreatedAt = DateTime.UtcNow;
    }
}
