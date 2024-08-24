using BrainwaveBandits.WinerR.Application.Common.Interfaces;
using BrainwaveBandits.WinerR.Application.Helpers;
using BrainwaveBandits.WinerR.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using BrainwaveBandits.WinerR.Domain.Entities;

namespace BrainwaveBandits.WinerR.Application.ImportedWines.Queries.SearchImportedWines;
public record SearchImportedWinesQuery : IRequest<IEnumerable<ImportedWineSearchResultDto>>
{
    public required string SearchQuery { get; init; }
}

public class SearchImportedWinesQueryHandler : IRequestHandler<SearchImportedWinesQuery, IEnumerable<ImportedWineSearchResultDto>>
{
    private readonly IApplicationDbContext _context;

    public SearchImportedWinesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ImportedWineSearchResultDto>> Handle(SearchImportedWinesQuery request, CancellationToken cancellationToken)
    {
        List<ImportedWine> searchResults = await _context.ImportedWines.ToListAsync();

        searchResults = searchResults
            .Where(x => x.WineID.InvariantContains(request.SearchQuery) ||
                        x.WineName.InvariantContains(request.SearchQuery) ||
                        x.WineryName.InvariantContains(request.SearchQuery))
            .OrderBy(x => x.WineryName)
            .Take(20)
            .ToList();

        if (searchResults != null && searchResults.Any())
        {
            return searchResults.Select(x => new ImportedWineSearchResultDto
            {
                WineId = x.WineID,
                Title = $"{x.WineryName} - {x.WineName}"
            });
        }

        return new List<ImportedWineSearchResultDto>();
    }
}
