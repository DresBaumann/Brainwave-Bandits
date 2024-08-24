using BrainwaveBandits.WinerR.Domain.Entities;

namespace BrainwaveBandits.WinerR.Application.Common.Interfaces;
public interface IOpenAIService
{
    Task<Recipe> GetRecipeFromDishNameAsync(string dishName);
}

