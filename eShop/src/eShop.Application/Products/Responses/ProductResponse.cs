namespace eShop.Domain.Products.Responses;

public record ProductResponse
{
    public Guid Id { get; init; }

    public string? Name { get; init; }

    public int? Sku { get; init; }

    public double? PriceAmount { get; init; }

    public PriceCurrencyResponse? PriceCurrency { get; init; }
}
