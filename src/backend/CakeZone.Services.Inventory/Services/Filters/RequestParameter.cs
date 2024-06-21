namespace CakeZone.Services.Inventory.Services.Filters
{
    public abstract class RequestParameter
    {
        private const int maxpPageSize = 50;

        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxpPageSize) ? maxpPageSize : value;
        }
    }
}
