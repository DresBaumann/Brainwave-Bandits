using BrainwaveBandits.WinerR.Domain.Events;
using Microsoft.Extensions.Logging;

namespace BrainwaveBandits.WinerR.Application.Wines.EventHandlers;

public class WineCompletedEventHandler : INotificationHandler<WineCompletedEvent>
{
    private readonly ILogger<WineCompletedEventHandler> _logger;

    public WineCompletedEventHandler(ILogger<WineCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(WineCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("BrainwaveBandits.WinerR Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
