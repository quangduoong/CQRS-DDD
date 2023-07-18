using eShop.Domain.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace eShop.Domain.Shared;
public class AvailableCurrencies
{
    public List<PriceCurrency> Values { get; } = new();

    public AvailableCurrencies(string jsonPath)
    {
        StreamReader streamReader = new(jsonPath);
        string json = streamReader.ReadToEnd();
        JsonSerializerOptions options = new()
        {
            ReferenceHandler = ReferenceHandler.Preserve
        };
        Values = JsonSerializer.Deserialize<List<PriceCurrency>>(json, options)!;
    }
}
