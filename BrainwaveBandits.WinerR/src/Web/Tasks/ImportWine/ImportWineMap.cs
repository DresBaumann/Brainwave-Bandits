using BrainwaveBandits.WinerR.Domain.Entities;
using CsvHelper.Configuration;

namespace BrainwaveBandits.WinerR.Web.Tasks.ImportWine;
internal sealed class ImportedWineMap : ClassMap<ImportedWine>
{
    public ImportedWineMap()
    {
        Map(m => m.WineID).Name("WineID");
        Map(m => m.WineName).Name("WineName");
        Map(m => m.WineryName).Name("WineryName");
        Map(m => m.Vintages).Name("Vintages");
    }
}
