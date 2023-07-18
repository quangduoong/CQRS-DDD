using eShop.Domain.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace eShop.Domain.Shared;
public class AvailableCurrencies
{
    public List<PriceCurrency> Values { get; } = new();

    public AvailableCurrencies()
    {
        StreamReader streamReader = new("../../currencies.json");
        string json = streamReader.ReadToEnd();
        JsonSerializerOptions options = new()
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };
        Values = JsonSerializer.Deserialize<List<PriceCurrency>>(json, options)!;
    }
}
