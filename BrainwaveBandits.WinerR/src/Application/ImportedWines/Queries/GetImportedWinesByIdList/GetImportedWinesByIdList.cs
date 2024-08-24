using System.Linq;
using BrainwaveBandits.WinerR.Application.Common.Interfaces;
using BrainwaveBandits.WinerR.Application.Wines.Queries.GetWinesWithPagination;
using BrainwaveBandits.WinerR.Domain.Entities;

namespace BrainwaveBandits.WinerR.Application.ImportedWines.Queries.GetImportedWinesByIdList;
public record GetImportedWinesByIdListQuery : IRequest<IEnumerable<ImportedWineBriefDto>>
{
    public IEnumerable<string>? WineIdList { get; init; }
}

public class GetImportedWinesByIdListQueryHandler : IRequestHandler<GetImportedWinesByIdListQuery, IEnumerable<ImportedWineBriefDto>>
{
    private readonly IApplicationDbContext _context;

    public GetImportedWinesByIdListQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ImportedWineBriefDto>> Handle(GetImportedWinesByIdListQuery request, CancellationToken cancellationToken)
    {
        if (request.WineIdList == null || !request.WineIdList.Any())
        {
            return new List<ImportedWineBriefDto>();
        }

        List<ImportedWine> searchResults = await _context
            .ImportedWines
            .Where(m => request.WineIdList.Contains(m.WineID))
            .Take(50)
            .ToListAsync();

        if (searchResults == null || !searchResults.Any())
        {
            return new List<ImportedWineBriefDto>();
        }

        return searchResults
            .Select(x => new ImportedWineBriefDto
            {
                WineId = x.WineID,
                WineName = x.WineName,
                WineryName = x.WineryName
            });
    }
}
