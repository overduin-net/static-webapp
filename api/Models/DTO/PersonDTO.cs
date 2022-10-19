namespace StaticWebApp.Template
{
    public record PersonDTO
    {
        public string RowKey { get; set; }
        public string PartitionKey { get; set; }
        public string Name { get; init; }
        public string Email { get; init; }
    }
}