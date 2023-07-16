using eShop.Domain.Entities;

namespace eShop.Infrastructure.Products.Responses;

public record ProductResponse
{
    public Guid Id { get; private set; }
    public string? Name { get; private set; }
    public int? Sku { get; private set; }
    public double? PriceAmount { get; private set; }
    public PriceCurrency PriceCurrency { get; private set; }
}
