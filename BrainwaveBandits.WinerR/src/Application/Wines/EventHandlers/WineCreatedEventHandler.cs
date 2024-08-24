using BrainwaveBandits.WinerR.Domain.Events;
using Microsoft.Extensions.Logging;

namespace BrainwaveBandits.WinerR.Application.Wines.EventHandlers;

public class WineCreatedEventHandler : INotificationHandler<WineCreatedEvent>
{
    private readonly ILogger<WineCreatedEventHandler> _logger;

    public WineCreatedEventHandler(ILogger<WineCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(WineCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("BrainwaveBandits.WinerR Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
