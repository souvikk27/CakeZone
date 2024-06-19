namespace CakeZone.Services.Inventory.Model
{
    public sealed class Inventory
    {
        public Guid ProductId { get; set; }

        public Guid StorageDepotId { get; set; }

        public int CurrentLevel { get; set; }

        public int MaxLevel { get; set; }

        public int MinLevel { get; set; }

        public int AverageDemand { get; set; }

        public int StandardDeviationDemand { get; set; }

        public int Demand { get; set; }

        public int LeadTime { get; set; }

        public int? OrderQuantity { get; set; }

        public int? OrderFrequency { get; set; }

        public decimal HoldingCostPerUnit { get; set; }

        public decimal? OrderingCostPerOrder { get; set; }

        public decimal ShortageCostPerUnit { get; set; }

        public int InventoryPosition { get; set; }

        public int? OrdersOutstanding { get; set; }

        public int? UnitsShort { get; set; }
    }
}