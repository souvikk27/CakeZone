using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Inventory.Shared.Supplier;

public class CreateSupplierDto
{
    public Guid Id = Guid.NewGuid();

    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Phone { get; set; }

    [Required]
    public string Address { get; set; }

    public DateTime CreatedAt = DateTime.Now;
}