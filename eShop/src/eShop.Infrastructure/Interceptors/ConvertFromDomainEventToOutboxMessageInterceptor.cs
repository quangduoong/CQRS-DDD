using eShop.Domain.Primitives;
using eShop.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace eShop.Infrastructure.Interceptors;

public class ConvertFromDomainEventToOutboxMessageInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        DbContext? dbContext = eventData.Context;

        if (dbContext is not null)
        {
            List<OutboxMessage> domainEvents = dbContext.ChangeTracker
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

            dbContext.Set<OutboxMessage>().AddRange(domainEvents);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}

