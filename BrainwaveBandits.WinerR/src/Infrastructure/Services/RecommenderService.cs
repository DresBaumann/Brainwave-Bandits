using System.Text;
using System.Text.Json;
using BrainwaveBandits.WinerR.Application.Common.Interfaces;
using BrainwaveBandits.WinerR.Domain.Entities;
using BrainwaveBandits.WinerR.Infrastructure.Data.Configurations;
using Microsoft.Extensions.Options;

namespace BrainwaveBandits.WinerR.Infrastructure.Services;

public class RecommenderService : IRecommenderService
{
    private readonly string _endpointUrl;
    private readonly HttpClient _httpClient;

    public RecommenderService(IOptions<RecommenderSystemOptions> options, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _endpointUrl = options.Value.EndpointUrl;
    }

    public async Task<List<int>> GetWineRecommendationsAsync(Recipe recipe)
    {
        StringContent requestContent = new StringContent(
            JsonSerializer.Serialize(recipe),
            Encoding.UTF8,
            "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync(_endpointUrl, requestContent);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error occurred while calling recommender system: {response.ReasonPhrase}");
        }

        string responseContent = await response.Content.ReadAsStringAsync();
        List<int>? recommendedWineIds = JsonSerializer.Deserialize<List<int>>(responseContent);

        if (recommendedWineIds == null)
        {
            throw new Exception("Failed to deserialize the recommender system response.");
        }

        return recommendedWineIds;
    }
}
