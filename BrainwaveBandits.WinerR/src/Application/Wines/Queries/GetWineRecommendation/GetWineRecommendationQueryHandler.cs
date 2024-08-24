using System.Collections.ObjectModel;
using BrainwaveBandits.WinerR.Application.Common.Interfaces;
using BrainwaveBandits.WinerR.Application.Recipes.Queries;
using BrainwaveBandits.WinerR.Application.Wines.Queries.GetWinesFromIds.cs;
using BrainwaveBandits.WinerR.Application.Wines.Queries.GetWinesWithPagination;
using BrainwaveBandits.WinerR.Domain.Entities;

namespace BrainwaveBandits.WinerR.Application.Wines.Queries.GetWineRecommendation;

public class GetWineRecommendationQueryHandler : IRequestHandler<GetWineRecommendationQuery, Collection<WineBriefDto>>
{
    private readonly ISender _sender;

    private readonly IRecommenderService _recommenderService;

    public GetWineRecommendationQueryHandler(ISender sender, IRecommenderService recommenderService)
    {
        _sender = sender;
        _recommenderService = recommenderService;
    }

    public async Task<Collection<WineBriefDto>> Handle(GetWineRecommendationQuery request, CancellationToken cancellationToken)
    {
        var recipe = await _sender.Send(new GetRecipeFromDishNameQuery() { DishName = request.DishName }, cancellationToken);


        List<int> recommendedWineIds = await _recommenderService.GetWineRecommendationsAsync(recipe);

        Collection<Wine> wines = await _sender.Send(new GetWinesFromIdsQuery(), cancellationToken);
        // Get wines from DB with Integer
        // Use Query 


        // map wines

        return new Collection<WineBriefDto>();
    }
}
