using BrainwaveBandits.WinerR.Application.Recipes.Queries;

namespace BrainwaveBandits.WinerR.Web.Endpoints;

public class Recipe : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetIRecipeFromDishName);
    }

    public async Task<Domain.Entities.Recipe> GetIRecipeFromDishName(ISender sender, [AsParameters] GetRecipeFromDishNameQuery query)
    {
        return await sender.Send(query);
    }
}
