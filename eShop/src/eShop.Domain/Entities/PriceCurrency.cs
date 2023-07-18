namespace eShop.Domain.Entities;

public record PriceCurrency
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public virtual ICollection<Product> Products { get; init; } = null!;
}