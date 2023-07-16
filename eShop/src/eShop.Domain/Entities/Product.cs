using System.ComponentModel.DataAnnotations;

namespace eShop.Domain.Entities;

public sealed class Product
{
    public Guid Id { get; private set; }

    public string? Name { get; private set; }

    public int? Sku { get; private set; }

    public double? PriceAmount { get; private set; }

    public PriceCurrency PriceCurrency { get; private set; }

    private Product() { }

    public static Product? Create(Guid id, string name, int sku, double priceAmount, PriceCurrency priceCurrency)
    {
        return new()
        {
            Id = id,
            Name = name,
            Sku = sku,
            PriceAmount = priceAmount,
            PriceCurrency = priceCurrency
        };
    }
}
