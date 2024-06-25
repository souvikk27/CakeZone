using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Inventory.Shared.StockReceipt
{
    public class UpdateStockReceiptDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Supplier is required")]
        public Guid SupplierId { get; set; }

        [Required(ErrorMessage = "Product is required")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Storage Depot is required")]
        public Guid StorageDepotId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public string Quantity { get; set; }

        [Required(ErrorMessage = "Receipt Date is required")]
        public DateTime ReceiptDate { get; set; }

        [Required(ErrorMessage = "Created Date is required")]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt = DateTime.Now;
    }
}