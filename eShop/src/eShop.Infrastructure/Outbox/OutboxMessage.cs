using eShop.Domain.Primitives;

namespace eShop.Infrastructure.Outbox;

public class OutboxMessage : Entity
{
    public string Type { get; } = default!;

    public string Content { get; } = default!;

    public DateTime? OccurredOnUtc { get; }

    public DateTime? ProcessedOnUtc { get; set; }

    public string? Error { get; }

    private OutboxMessage() { }

    public OutboxMessage(Guid id, string type, string content, DateTime occurredOnUtc) : base(id)
    {
        Type = type;
        Content = content;
        OccurredOnUtc = occurredOnUtc;
    }
}

