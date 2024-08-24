using System.Collections.ObjectModel;
using BrainwaveBandits.WinerR.Application.Wines.Queries.GetWinesWithPagination;

namespace BrainwaveBandits.WinerR.Application.Wines.Queries.GetWineRecommendation;
public record GetWineRecommendationQuery : IRequest<Collection<WineBriefDto>>
{
    public string DishName { get; set; } = null!;
}
