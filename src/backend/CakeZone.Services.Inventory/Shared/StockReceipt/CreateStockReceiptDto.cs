using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Inventory.Shared.StockReceipt
{
    public class CreateStockReceiptDto
    {
        public Guid Id = Guid.NewGuid();

        [Required(ErrorMessage = "Supplier is required")]
        public Guid SupplierId { get; set; }

        [Required(ErrorMessage = "Product is required")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Storage Depot is required")]
        public Guid StorageDepotId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

        public DateTime ReceiptDate = DateTime.Now;

        public DateTime CreatedAt = DateTime.Now;
    }
}