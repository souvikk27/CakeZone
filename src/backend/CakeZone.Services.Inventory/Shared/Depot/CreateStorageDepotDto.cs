using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Inventory.Shared.Depot
{
    public class CreateStorageDepotDto
    {
        public Guid Id = Guid.NewGuid();

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        public int Capacity { get; set; }

        public DateTime CreatedAt = DateTime.Now;
    }
}
