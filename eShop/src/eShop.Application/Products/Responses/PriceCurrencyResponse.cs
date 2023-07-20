namespace eShop.Application.Products.Responses;

public record PriceCurrencyResponse
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;
}
