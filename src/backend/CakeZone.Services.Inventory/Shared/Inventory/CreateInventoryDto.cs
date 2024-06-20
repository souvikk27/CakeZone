using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Inventory.Shared.Inventory
{
    public class CreateInventoryDto
    {
        public Guid ProductId { get; set; }

        public Guid StorageDepotId { get; set; }

        [Required(ErrorMessage = "Current Level is required")]
        public int CurrentLevel { get; set; }

        [Required(ErrorMessage = "Minimum Level is required")]
        public int MinLevel { get; set; }

        [Required(ErrorMessage = "Demand is required")]
        public int Demand { get; set; }

        [Required(ErrorMessage = "Holding Cost Per Unit is required")]
        public decimal HoldingCostPerUnit { get; set; }
    }
}