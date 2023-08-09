using eShop.Domain.Primitives;
using eShop.Infrastructure.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;

namespace eShop.Infrastructure.BackgroundJobs;

public class ProcessOutboxMessageJob : IJob
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<ProcessOutboxMessageJob> _logger;
    private readonly IPublisher _publisher;

    public ProcessOutboxMessageJob(
        AppDbContext dbContext,
        ILogger<ProcessOutboxMessageJob> logger,
        IPublisher publisher)
    {
        _dbContext = dbContext;
        _logger = logger;
        _publisher = publisher;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        IReadOnlyCollection<OutboxMessage> outboxMessages = await _dbContext.OutboxMessages
            .Where(m => m.ProcessedOnUtc == null)
            .Take(20)
            .OrderBy(outboxMessage => outboxMessage.OccurredOnUtc)
            .ToListAsync();

        foreach (var outboxMessage in outboxMessages)
        {
            IDomainEvent? domainEvent =
                JsonConvert.DeserializeObject<IDomainEvent>(
                    outboxMessage.Content,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                    });

            if (domainEvent is null)
            {
                _logger.LogError(
                    $"Event {outboxMessage.Id} has null content.",
                    nameof(outboxMessage));
                continue;
            }

            await _publisher.Publish(domainEvent);

            outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
            _dbContext.Update(outboxMessage);
        }

        await _dbContext.SaveChangesAsync();
    }
}
