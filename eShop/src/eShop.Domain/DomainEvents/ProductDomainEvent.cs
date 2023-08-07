using eShop.Domain.Primitives;

namespace eShop.Domain.DomainEvents;

public sealed class ProductDomainEvent
{
    public sealed record Created(Guid Id) : IDomainEvent { }
}

