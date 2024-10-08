﻿using System.Collections.ObjectModel;
using BrainwaveBandits.WinerR.Application.Common.Interfaces;
using BrainwaveBandits.WinerR.Domain.Entities;

namespace BrainwaveBandits.WinerR.Application.Wines.Queries.GetWinesFromIds.cs;

public class GetWinesFromIdsQueryHandler : IRequestHandler<GetWinesFromIdsQuery, Collection<Wine>>
{
    private readonly IApplicationDbContext _context;

    public GetWinesFromIdsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Collection<Wine>> Handle(GetWinesFromIdsQuery request, CancellationToken cancellationToken)
    {
        var wines = await _context.Wines
            .Where(w => request.Ids.Contains(w.WineId))
            .ToListAsync(cancellationToken);
        
        return new Collection<Wine>(wines);
    }
}
