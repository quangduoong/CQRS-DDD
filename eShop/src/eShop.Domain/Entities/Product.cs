
namespace eShop.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public int Sku { get; private set; } = 0;

    public double PriceAmount { get; private set; } = 0;

    public Guid PriceCurrencyId { get; private set; }

    public virtual PriceCurrency PriceCurrency { get; private set; } = null!;

    private Product() { }

    public Product(Guid id, string name, int sku, double priceAmount, Guid priceCurrencyId, PriceCurrency priceCurrency)
    {
        Id = id;
        Name = name;
        Sku = sku;
        PriceAmount = priceAmount;
        PriceCurrencyId = priceCurrencyId;
        PriceCurrency = priceCurrency;
    }
}
