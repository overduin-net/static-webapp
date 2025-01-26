public record Person : AbstractTableEntity
{
    public string Name { get; init; } = default!;
    public string Email { get; init; } = default!;
}