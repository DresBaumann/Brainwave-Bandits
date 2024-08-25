using BrainwaveBandits.WinerR.Application.Common.Interfaces;
using BrainwaveBandits.WinerR.Application.Common.Mappings;
using BrainwaveBandits.WinerR.Application.Common.Models;

namespace BrainwaveBandits.WinerR.Application.Wines.Queries.GetWinesWithPagination;

public record GetWinesWithPaginationQuery : IRequest<PaginatedList<WineBriefDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetWinesWithPaginationQueryHandler : IRequestHandler<GetWinesWithPaginationQuery, PaginatedList<WineBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetWinesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<WineBriefDto>> Handle(GetWinesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Wines
            .OrderBy(x => x.Name)
            .ProjectTo<WineBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
