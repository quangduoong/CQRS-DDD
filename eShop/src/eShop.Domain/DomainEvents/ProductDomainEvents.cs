using eShop.Domain.Primitives;

namespace eShop.Domain.DomainEvents;

public sealed class ProductDomainEvents
{
    public sealed record Created(Guid Id) : IDomainEvent { }
}

