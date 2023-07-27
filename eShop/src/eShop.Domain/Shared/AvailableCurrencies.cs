using eShop.Domain.Entities;

namespace eShop.Domain.Shared;

public class AvailableCurrencies
{
    public PriceCurrency VND { get; } = new(
        Guid.Parse("1491857a-9602-44c4-bebb-80ef5e0ca81e"),
        "VND",
        "Vietnamese currency.");

    public PriceCurrency USD { get; } = new(
        Guid.Parse("e347c43a-a547-42be-b134-5874454109a5"),
        "USD",
        "United State's currency.");

    public List<PriceCurrency> GetAll() => new() { VND, USD };
}
