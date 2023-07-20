namespace eShop.Application.Products.Responses;

public record ProductQueryResponse
{
    public Guid Id { get; init; }

    public string? Name { get; init; } = string.Empty;

    public int Sku { get; init; } = 0;

    public double PriceAmount { get; init; } = 0;

    public PriceCurrencyResponse PriceCurrency { get; init; } = null!;
}
