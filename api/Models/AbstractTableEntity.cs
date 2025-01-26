public abstract record AbstractTableEntity : ITableEntity
{
    public string RowKey { get; set; } = default!;
    public string PartitionKey { get; set; } = default!;
    ETag ITableEntity.ETag { get; set; } = default!;
    public DateTimeOffset? Timestamp { get; set; } = default!;
    public DateTime? CreatedDate { get; set; } = default!;
    public string CreatedBy { get; set; } = default!;
    public DateTime? ModifiedDate { get; set; } = default!;
    public string ModifiedBy { get; set; } = default!;
}