using BrainwaveBandits.WinerR.Domain.Entities;

namespace BrainwaveBandits.WinerR.Application.Common.Interfaces;
public interface IRecommenderService
{
    Task<List<int>> GetWineRecommendationsAsync(Recipe request);
}

