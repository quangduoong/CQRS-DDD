namespace eShop.Domain.Products.Requests;

public record CreateProductRequest
{
    public string? Name { get; init; }

    public int? Sku { get; init; }

    public double? PriceAmount { get; init; }

    public Guid? PriceCurrencyId { get; init; }
}
