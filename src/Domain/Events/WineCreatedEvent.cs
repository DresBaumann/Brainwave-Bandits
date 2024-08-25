namespace BrainwaveBandits.WinerR.Domain.Events;

public class WineCreatedEvent : BaseEvent
{
    public WineCreatedEvent(Wine wine)
    {
        Wine = wine;
    }

    public Wine Wine { get; }
}
