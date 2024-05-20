﻿namespace CakeZone.Services.Product.Shared.Products
{
    public record ProductViewDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt {  get; set; }
        public bool IsDeleted { get; set; }
    }
}