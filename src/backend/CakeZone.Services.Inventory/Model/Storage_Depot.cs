using System.ComponentModel.DataAnnotations;

namespace CakeZone.Services.Inventory.Model
{
    public sealed class Storage_Depot
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}