using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Inventory.Model
{
    public class StockIssue
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid Storage_DepotId { get; set; }
        public int Quantity { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Inventory Inventory { get; set; }
        public Storage_Depot Storage_Depot { get; set; }
    }
}
