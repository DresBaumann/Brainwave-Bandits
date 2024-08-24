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
    private readonly IWebHostEnvironment _hostingEnvironment;

    public ImportWineTask(IApplicationDbContext context, ISender sender, IWebHostEnvironment hostingEnvironment)
    {
        _context = context;
        _sender = sender;
        _hostingEnvironment = hostingEnvironment;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "App_Data", "wines.csv");
        using (StreamReader reader = new StreamReader(filePath))
        using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Context.RegisterClassMap<ImportedWineMap>();
            IAsyncEnumerable<ImportedWine> importedWineRecords = csv.GetRecordsAsync<ImportedWine>();
            List<ImportedWine> importedWines = await importedWineRecords.ToListAsync();
            foreach (ImportedWine importedWine in importedWines)
            {
                CreateImportedWineCommand createImportedWineCommand = new CreateImportedWineCommand
                {
                    WineId = importedWine.WineID,
                    WineName = importedWine.WineName,
                    WineryName = importedWine.WineryName,
                    Vintages = importedWine.Vintages
                };

                await _sender.Send(createImportedWineCommand, cancellationToken);
            }
        }
    }
}
