using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Inventory.Model
{
    public class StockIssue
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public Guid StorageDepotId { get; set; }

        public int Quantity { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public virtual Inventory Inventory { get; set; } = null!;
    }
}
