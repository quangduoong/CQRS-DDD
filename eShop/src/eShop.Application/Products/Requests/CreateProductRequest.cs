namespace eShop.Domain.Products.Requests;

public record CreateProductRequest
{
    public string Name { get; init; } = string.Empty;

    public int Sku { get; init; } = 0;

    public double PriceAmount { get; init; } = 0;

    public Guid PriceCurrencyId { get; init; }
}
