using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrainwaveBandits.WinerR.Application.Common.Interfaces;
using BrainwaveBandits.WinerR.Application.ImportedWines.Queries.GetImportedWinesByIdList;
using BrainwaveBandits.WinerR.Application.Wines.Commands.CreateWine;
using BrainwaveBandits.WinerR.Application.Wines.Commands.UpdateWine;
using BrainwaveBandits.WinerR.Domain.Entities;
using BrainwaveBandits.WinerR.Domain.Events;

namespace BrainwaveBandits.WinerR.Application.Wines.Commands.CreateOrUpdateWinesByIdList;
public record CreateOrUpdateWinesByIdListCommand : IRequest<List<int>>
{
    public required List<string> WineIdList { get; init; }
}

public class CreateOrUpdateWinesByIdListCommandHandler : IRequestHandler<CreateOrUpdateWinesByIdListCommand, List<int>>
{
    private readonly IApplicationDbContext _context;
    private readonly ISender _sender;

    public CreateOrUpdateWinesByIdListCommandHandler(IApplicationDbContext context, ISender sender)
    {
        _context = context;
        _sender = sender;
    }

    public async Task<List<int>> Handle(CreateOrUpdateWinesByIdListCommand request, CancellationToken cancellationToken)
    {
        List<int> createdOrUpdatedWines = new List<int>();

        List<ImportedWine> searchResults = await _context
            .ImportedWines
            .Where(m => request.WineIdList.Contains(m.WineID))
            .ToListAsync();

        foreach (ImportedWine importedWine in searchResults)
        {
            if (_context.Wines.Any(w => w.WineId == importedWine.WineID))
            {
                Wine existingWine = _context.Wines.First(w => w.WineId == importedWine.WineID);
                UpdateWineCommand updateWineCommand = new UpdateWineCommand
                {
                    WineID = importedWine.WineID,
                    Name = importedWine.WineName,
                    Brand = importedWine.WineryName,
                    Amount = existingWine.Amount + 1
                };
                await _sender.Send(updateWineCommand);
                createdOrUpdatedWines.Add(existingWine.Id);
            }
            else
            {
                CreateWineCommand createWineCommand = new CreateWineCommand
                {
                    WineId = importedWine.WineID,
                    Name = importedWine.WineName,
                    Brand = importedWine.WineryName,
                    Amount = 1
                };
                int createdWineId = await _sender.Send(createWineCommand);
                createdOrUpdatedWines.Add(createdWineId);
            }
        }
            
        return createdOrUpdatedWines;
    }
}
