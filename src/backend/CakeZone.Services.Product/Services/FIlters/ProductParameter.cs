namespace CakeZone.Services.Product.Services.Filters
{
    public class ProductParameter : RequestParameter
    {
        public string? ProductName { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
