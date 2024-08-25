using BrainwaveBandits.WinerR.Application.ImportedWines.Commands.CreateImportedWine;
using CsvHelper;
using System.Globalization;

namespace BrainwaveBandits.WinerR.Web.Tasks.ImportWine;

public class ImportWineTask
{
    private readonly ISender _sender;

    public ImportWineTask(ISender sender)
    {
        _sender = sender;
    }

    public async Task<int> ExecuteAsync()
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

                await _sender.Send(createImportedWineCommand);
            }

            return importedWineDtos.Count;
        }
    }
}
