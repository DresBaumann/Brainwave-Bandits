using System.Collections.ObjectModel;
using BrainwaveBandits.WinerR.Domain.Entities;

namespace BrainwaveBandits.WinerR.Application.Wines.Queries.GetWinesFromIds.cs;

public class GetWinesFromIdsQuery : IRequest<Collection<Wine>>
{
    public Collection<string> Ids { get; set; } = null!;
}
