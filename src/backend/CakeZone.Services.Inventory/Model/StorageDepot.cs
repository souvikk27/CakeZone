using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Inventory.Model
{
    public class StorageDepot
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public int Capacity { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

        public virtual ICollection<StockReceipt> StockReceipts { get; set; } = new List<StockReceipt>();
    }
}
