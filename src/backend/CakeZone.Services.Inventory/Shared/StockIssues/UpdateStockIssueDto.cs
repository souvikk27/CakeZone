using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Inventory.Shared.StockIssues
{
    public class UpdateStockIssueDto
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Product Id is required")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Storage Depot Id is required")]
        public Guid StorageDepotId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Issue date is required")]
        public DateTime IssueDate { get; set; }

        [Required(ErrorMessage = "Created date time is required")] 
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt = DateTime.Now;
    }
}
