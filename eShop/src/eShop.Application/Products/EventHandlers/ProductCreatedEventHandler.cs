using eShop.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eShop.Application.Products.EventHandlers;

public class ProductCreatedEventHandler : INotificationHandler<ProductDomainEvent.Created>
{
    private readonly ILogger<ProductCreatedEventHandler> _logger;

    public ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(ProductDomainEvent.Created notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("\"Product created\" event raised");
        await Task.CompletedTask;
    }
}

