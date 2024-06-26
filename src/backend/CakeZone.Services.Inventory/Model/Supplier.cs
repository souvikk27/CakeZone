﻿using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Inventory.Model
{
    public class Supplier
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Address { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<StockReceipt> StockReceipts { get; set; } = new List<StockReceipt>();
    }
}
