namespace BrainwaveBandits.WinerR.Domain.Entities;
public class Wine : BaseAuditableEntity
{
    public required string WineId { get; set; } = null!;
    public required string Name { get; set; } = null!;
    public string? Brand { get; set; } = null!;
    public int Vintage { get; set; } = 0;
    public int Amount { get; set; } = 0;

    private bool _done;
    public bool Done
    {
        get => _done;
        set
        {
            if (value && !_done)
            {
                AddDomainEvent(new WineCompletedEvent(this));
            }

            _done = value;
        }
    }
}
