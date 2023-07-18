
namespace eShop.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }

    public string? Name { get; private set; }

    public int? Sku { get; private set; }

    public double? PriceAmount { get; private set; }

    public Guid PriceCurrencyId { get; private set; }

    public virtual PriceCurrency? PriceCurrency { get; private set; }

    private Product() { }

    public static Product? Create(Guid id, string name, int sku, double priceAmount, Guid priceCurrencyId)
    {
        return new()
        {
            Id = id,
            Name = name,
            Sku = sku,
            PriceAmount = priceAmount,
            PriceCurrencyId = priceCurrencyId
        };
    }
}
