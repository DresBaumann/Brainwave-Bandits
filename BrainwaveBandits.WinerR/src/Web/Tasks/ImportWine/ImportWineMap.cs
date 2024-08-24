using BrainwaveBandits.WinerR.Application.ImportedWines.Commands.CreateImportedWine;
using CsvHelper.Configuration;

namespace BrainwaveBandits.WinerR.Web.Tasks.ImportWine;
internal sealed class ImportedWineMap : ClassMap<ImportedWineDto>
{
    public ImportedWineMap()
    {
        Map(m => m.WineId).Name("WineID");
        Map(m => m.WineName).Name("WineName");
        Map(m => m.WineryName).Name("WineryName");
        Map(m => m.Vintages).Name("Vintages");
    }
}
