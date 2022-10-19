using System;
using Azure.Data.Tables;
using Azure;

namespace StaticWebApp.Template
{
    public record Person : ITableEntity
    {
        public string RowKey { get; set; } = default!;
        public string PartitionKey { get; set; } = default!;
        public string Name { get; init; } = default!;
        public string Email { get; init; } = default!;
        ETag ITableEntity.ETag { get; set; } = default!;
        public DateTimeOffset? Timestamp { get; set; } = default!;
    }
}