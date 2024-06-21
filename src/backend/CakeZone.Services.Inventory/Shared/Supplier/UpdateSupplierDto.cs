using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Inventory.Shared.Supplier;

public record UpdateSupplierDto
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Phone { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt = DateTime.Now;
}