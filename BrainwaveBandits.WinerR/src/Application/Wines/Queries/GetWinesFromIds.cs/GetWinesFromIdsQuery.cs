using System.Collections.ObjectModel;
using BrainwaveBandits.WinerR.Domain.Entities;

namespace BrainwaveBandits.WinerR.Application.Wines.Queries.GetWinesFromIds.cs;

public class GetWinesFromIdsQuery : IRequest<Collection<Wine>>
{
    public Collection<int> Ids { get; set; } = null!;
}
