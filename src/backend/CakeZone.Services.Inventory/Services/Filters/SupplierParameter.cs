namespace CakeZone.Services.Inventory.Services.Filters;

public class SupplierParameter : RequestParameter
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public DateTime AddedOn { get; set; }
}