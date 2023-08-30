using eShop.Domain.Abstractions;
using eShop.Domain.Primitives;
using eShop.Infrastructure.Database;
using eShop.Infrastructure.Outbox;
using Newtonsoft.Json;

namespace eShop.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ConvertFromDomainEventToOutboxMessageInterceptor();

        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    private void ConvertFromDomainEventToOutboxMessageInterceptor()
    {
        List<OutboxMessage> domainEvents = _dbContext.ChangeTracker
            .Entries<AggregateRoot>()
            .Select(x => x.Entity)
            .SelectMany(aggRoot =>
            {
                IReadOnlyCollection<IDomainEvent> domainEvents = aggRoot.GetDomainEvents();
                aggRoot.ClearDomainEvents();
                return domainEvents;
            })
            .Select(domainEvt => new OutboxMessage(
                Guid.NewGuid(),
                domainEvt.GetType().Name,
                JsonConvert.SerializeObject(domainEvt, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All,
                }),
                DateTime.UtcNow
                ))
            .ToList();

        _dbContext.Set<OutboxMessage>().AddRange(domainEvents);
    }

}

