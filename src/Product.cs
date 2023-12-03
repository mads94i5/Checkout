public record Product
{
    public string? EAN { get; init; }
    public string? Name { get; init; }
    public decimal Price { get; init; }
    public Tuple<int, decimal>? Discount { get; init; }
    public decimal Deposit { get; init; }
    public int Pieces { get; init; }
    public int Category { get; init; }
}