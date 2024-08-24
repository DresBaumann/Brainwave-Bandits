namespace BrainwaveBandits.WinerR.Domain.Events;

public class WineDeletedEvent : BaseEvent
{
    public WineDeletedEvent(Wine wine)
    {
        Wine = wine;
    }

    public Wine Wine { get; }
}
