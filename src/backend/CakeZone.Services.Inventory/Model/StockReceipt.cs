using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Inventory.Model
{
    public class StockReceipt
    {
        [Key]
        public Guid Id { get; set; }
        public Guid SupplierId { get; set; }
        public Guid ProductId { get; set; }
        public Guid StorageDepotId { get; set; }
        public int Quantity { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Storage_Depot Storage_Depot { get; set; }
        public Supplier Supplier { get; set; }
    }
}
