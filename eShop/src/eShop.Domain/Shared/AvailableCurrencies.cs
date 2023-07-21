using eShop.Domain.Entities;

namespace eShop.Domain.Shared;

public class AvailableCurrencies
{
    public PriceCurrency VND { get; } = new()
    {
        Id = Guid.Parse("1491857a-9602-44c4-bebb-80ef5e0ca81e"),
        Name = "VND",
        Description = "Vietnamese currency."
    };

    public PriceCurrency USD { get; } = new()
    {
        Id = Guid.Parse("e347c43a-a547-42be-b134-5874454109a5"),
        Name = "USD",
        Description = "United State's currency."
    };

    public List<PriceCurrency> GetAll() => new() { VND, USD };
}
