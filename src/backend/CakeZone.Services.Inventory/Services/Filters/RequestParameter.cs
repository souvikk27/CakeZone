﻿namespace CakeZone.Services.Inventory.Services.Filters
{
    public abstract class RequestParameter
    {
        const int maxpPageSize = 50;

        public int PageNumber { get; set; } = 1;
        private int _pagesize = 10;
        public int PageSize
        {
            get => _pagesize;
            set => _pagesize = (value > maxpPageSize) ? maxpPageSize : value;
        }
    }
}
