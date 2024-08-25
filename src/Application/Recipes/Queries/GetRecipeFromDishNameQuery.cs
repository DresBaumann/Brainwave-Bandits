using BrainwaveBandits.WinerR.Domain.Entities;

namespace BrainwaveBandits.WinerR.Application.Recipes.Queries;

public record GetRecipeFromDishNameQuery : IRequest<Recipe>
{
    public string DishName { get; set; } = null!;
}
