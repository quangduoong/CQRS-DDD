namespace eShop.Domain.Entities;

public record PriceCurrency
{
    public Guid Id { get; init; }

    public string? Name { get; init; }

    public string? Description { get; init; }

    public virtual ICollection<Product>? Products { get; init; }
}