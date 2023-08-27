using eShop.Domain.Primitives;

namespace eShop.Domain.DomainEvents;

public sealed class DomainEvents
{
    public sealed record ProductCreated(Guid Id) : IDomainEvent { }
}

