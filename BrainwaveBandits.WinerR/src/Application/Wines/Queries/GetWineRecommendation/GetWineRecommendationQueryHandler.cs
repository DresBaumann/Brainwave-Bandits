using System.Collections.ObjectModel;
using BrainwaveBandits.WinerR.Application.Recipes.Queries;
using BrainwaveBandits.WinerR.Application.Wines.Queries.GetWinesWithPagination;
using BrainwaveBandits.WinerR.Domain.Entities;

namespace BrainwaveBandits.WinerR.Application.Wines.Queries.GetWineRecommendation;

public class GetWineRecommendationQueryHandler : IRequestHandler<GetWineRecommendationQuery, Collection<WineBriefDto>>
{
    private readonly ISender _sender;

    public GetWineRecommendationQueryHandler(ISender sender)
    {
        _sender = sender;
    }

    public async Task<Collection<WineBriefDto>> Handle(GetWineRecommendationQuery request, CancellationToken cancellationToken)
    {
        var recipe = await _sender.Send(new GetRecipeFromDishNameQuery() { DishName = request.DishName }, cancellationToken);

        // API Call to Gioele returns List Integer

        // Get wines from DB with Integer
        // Use Query 

        return new Collection<WineBriefDto>();
    }
}
