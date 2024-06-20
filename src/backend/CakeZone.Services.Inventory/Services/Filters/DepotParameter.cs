namespace CakeZone.Services.Inventory.Services.Filters;

public class DepotParameter : RequestParameter
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public DateTime AddedOn { get; set; }
}