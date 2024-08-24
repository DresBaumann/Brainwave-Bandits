namespace BrainwaveBandits.WinerR.Domain.Entities;

public class ImportedWine : BaseAuditableEntity
{
    public required string WineID { get; set; }

    public required string WineName { get; set; }

    public required string WineryName { get; set; }

    public required string Vintages { get; set; }
}
