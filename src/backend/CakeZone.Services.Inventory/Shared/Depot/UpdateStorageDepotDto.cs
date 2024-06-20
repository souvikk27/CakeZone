using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Inventory.Shared.Depot;

public class UpdateStorageDepotDto
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; } = null!;

    [Required(ErrorMessage = "Capacity is required")]
    public int Capacity { get; set; }

    [Required(ErrorMessage = "Created Datetime is required")]
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt = DateTime.Now;
}