namespace SharedLibrary.Event
{
    public record ProductCreated
    {
        public Guid ProductId { get; init; }
        public Guid StorageDepotId { get; init; }
        public int MaxLevel { get; init; }
        public int CurrentLevel { get; init; }  
        public int MinLevel { get; init; }
    }
}
