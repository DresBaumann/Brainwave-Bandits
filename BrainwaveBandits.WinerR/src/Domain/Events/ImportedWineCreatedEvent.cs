namespace BrainwaveBandits.WinerR.Domain.Events;

public class ImportedWineCreatedEvent : BaseEvent
{
    public ImportedWineCreatedEvent(ImportedWine importedWine)
    {
        ImportedWine = importedWine;
    }

    public ImportedWine ImportedWine { get; }
}
