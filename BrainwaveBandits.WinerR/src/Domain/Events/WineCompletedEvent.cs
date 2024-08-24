namespace BrainwaveBandits.WinerR.Domain.Events;

public class WineCompletedEvent : BaseEvent
{
    public WineCompletedEvent(Wine wine)
    {
        Wine = wine;
    }

    public Wine Wine { get; }
}
