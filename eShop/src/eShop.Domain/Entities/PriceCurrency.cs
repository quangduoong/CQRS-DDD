using eShop.Domain.Primitives;
using Newtonsoft.Json;

namespace eShop.Domain.Entities;

public class PriceCurrency : Entity
{
    public string Name { get; init; }

    public string Description { get; init; }

    [JsonIgnore]
    public virtual ICollection<Product> Products { get; init; } = default!;

    public PriceCurrency(Guid id, string name, string description) : base(id)
    {
        Name = name;
        Description = description;
    }
}