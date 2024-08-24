using System.Globalization;
using BrainwaveBandits.WinerR.Application.ImportedWines.Commands.CreateImportedWine;
using BrainwaveBandits.WinerR.Application.ImportedWines.Commands.ImportWines;
using BrainwaveBandits.WinerR.Web.Tasks.ImportWine;
using CsvHelper;

namespace BrainwaveBandits.WinerR.Web.Endpoints;

public class ImportedWine : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(Import);
    }

    public async Task<int> Import(ISender sender, ImportWinesCommand command)
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "wines.csv");
        using (StreamReader reader = new StreamReader(filePath))
        using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Context.RegisterClassMap<ImportedWineMap>();
            IAsyncEnumerable<ImportedWineDto> importedWineRecords = csv.GetRecordsAsync<ImportedWineDto>();
            List<ImportedWineDto> importedWineDtos = await importedWineRecords.ToListAsync();
            CreateImportedWineCommand createImportedWineCommand;
            foreach (ImportedWineDto importedWineDto in importedWineDtos)
            {
                createImportedWineCommand = new()
                {
                    WineId = importedWineDto.WineId,
                    WineName = importedWineDto.WineName,
                    WineryName = importedWineDto.WineryName,
                    Vintages = importedWineDto.Vintages
                };

                await sender.Send(createImportedWineCommand);
            }

            return importedWineDtos.Count;
        }
    }
}
