public record Person : ITableEntity
{
    public string RowKey { get; set; } = default!;
    public string PartitionKey { get; set; } = Constants.PartitionKey;
    public string Name { get; init; } = default!;
    public string Email { get; init; } = default!;
    ETag ITableEntity.ETag { get; set; } = default!;
    public DateTimeOffset? Timestamp { get; set; } = default!;
}