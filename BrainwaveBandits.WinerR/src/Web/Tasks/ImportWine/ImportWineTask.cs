using System.Globalization;
using BrainwaveBandits.WinerR.Application.Common.Interfaces;
using BrainwaveBandits.WinerR.Application.ImportedWines.Commands.CreateImportedWine;
using BrainwaveBandits.WinerR.Domain.Entities;
using CsvHelper;

namespace BrainwaveBandits.WinerR.Web.Tasks.ImportWine;
public class ImportWineTask
{
    private readonly IApplicationDbContext _context;
    private readonly ISender _sender;

    public ImportWineTask(IApplicationDbContext context, ISender sender)
    {
        _context = context;
        _sender = sender;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using (StreamReader reader = new StreamReader("wines.csv"))
        using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Context.RegisterClassMap<ImportedWineMap>();
            IAsyncEnumerable<ImportedWine> importedWineRecords = csv.GetRecordsAsync<ImportedWine>();
            List<ImportedWine> importedWines = await importedWineRecords.ToListAsync();
            foreach (ImportedWine importedWine in importedWines)
            {
                CreateImportedWineCommand createImportedWineCommand = new CreateImportedWineCommand
                {
                    WineID = importedWine.WineID,
                    WineName = importedWine.WineName,
                    WineryName = importedWine.WineryName,
                    Vintages = importedWine.Vintages
                };

                await _sender.Send(createImportedWineCommand, cancellationToken);
            }
        }
    }
}
