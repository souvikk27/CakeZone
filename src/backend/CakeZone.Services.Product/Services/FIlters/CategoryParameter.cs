namespace CakeZone.Services.Product.Services.Filters;

public class CategoryParameter : RequestParameter
{
    public string? CategoryName { get; set; }
    public DateTime AddedOn { get; set; }
}