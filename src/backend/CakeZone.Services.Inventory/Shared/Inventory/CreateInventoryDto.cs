using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Inventory.Shared.Inventory
{
    public class CreateInventoryDto
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public Guid StorageDepotId { get; set; }

        [Required]
        public int CurrentLevel { get; set; }

        [Required]
        public int MaxLevel { get; set; }

        [Required]
        public int MinLevel { get; set; }

        [Required]
        public int AverageDemand { get; set; }

        [Required]
        public int StandardDeviationDemand { get; set; }

        [Required]
        public int Demand { get; set; }

        [Required]
        public int LeadTime { get; set; }

        public int? OrderQuantity { get; set; }

        public int? OrderFrequency { get; set; }

        [Required]
        public decimal HoldingCostPerUnit { get; set; }

        public decimal? OrderingCostPerOrder { get; set; }

        [Required]
        public decimal ShortageCostPerUnit { get; set; }

        [Required]
        public int InventoryPosition { get; set; }

        public int? OrdersOutstanding { get; set; }

        public int? UnitsShort { get; set; }
    }
}