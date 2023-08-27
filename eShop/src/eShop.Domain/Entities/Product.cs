using eShop.Domain.DomainEvents;
using eShop.Domain.Primitives;
using Newtonsoft.Json;

namespace eShop.Domain.Entities;

public class Product : AggregateRoot
{
    public string Name { get; private set; } = default!;

    public int Sku { get; private set; }

    public double PriceAmount { get; private set; }

    public Guid PriceCurrencyId { get; private set; }

    public virtual PriceCurrency PriceCurrency { get; private set; } = null!;

    public Product(
        Guid id,
        string name,
        int sku,
        double priceAmount,
        Guid priceCurrencyId) : base(id)
    {
        Name = name;
        Sku = sku;
        PriceAmount = priceAmount;
        PriceCurrencyId = priceCurrencyId;
    }

    [JsonConstructor]
    public Product(
        Guid id,
        string name,
        int sku,
        double priceAmount,
        PriceCurrency priceCurrency) : base(id)
    {
        Name = name;
        Sku = sku;
        PriceAmount = priceAmount;
        PriceCurrency = priceCurrency;
    }

    public static Product Create(
        Guid id,
        string name,
        int sku,
        double priceAmount,
        Guid priceCurrencyId)
    {
        Product newProduct = new(id, name, sku, priceAmount, priceCurrencyId);
        newProduct.RaiseDomainEvent(new DomainEvents.DomainEvents.ProductCreated(id));
        return newProduct;
    }
}
