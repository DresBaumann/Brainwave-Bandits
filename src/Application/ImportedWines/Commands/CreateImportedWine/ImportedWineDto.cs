namespace BrainwaveBandits.WinerR.Application.ImportedWines.Commands.CreateImportedWine;
public class ImportedWineDto
{
    public required string WineId { get; init; }

    public required string WineName { get; init; }

    public required string WineryName { get; init; }

    public required string Vintages { get; init; }
}
