using BrainwaveBandits.WinerR.Domain.Events;
using Microsoft.Extensions.Logging;

namespace BrainwaveBandits.WinerR.Application.Wines.EventHandlers;

public class ImportWinesCompletedEventHandler : INotificationHandler<ImportedWineCreatedEvent>
{
    private readonly ILogger<WineCreatedEventHandler> _logger;

    public ImportWinesCompletedEventHandler(ILogger<WineCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ImportedWineCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("BrainwaveBandits.WinerR Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
