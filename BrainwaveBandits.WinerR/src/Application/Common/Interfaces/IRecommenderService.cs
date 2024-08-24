using BrainwaveBandits.WinerR.Domain.Entities;

namespace BrainwaveBandits.WinerR.Application.Common.Interfaces;
public interface IRecommenderService
{
    Task<List<string>> GetWineRecommendationsAsync(Recipe request);
}

