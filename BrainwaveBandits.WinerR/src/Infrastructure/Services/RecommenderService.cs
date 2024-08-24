using System.Text;
using System.Text.Json;
using BrainwaveBandits.WinerR.Application.Common.Interfaces;
using BrainwaveBandits.WinerR.Domain.Entities;
using BrainwaveBandits.WinerR.Infrastructure.Data.Configurations;
using Microsoft.Extensions.Options;

namespace BrainwaveBandits.WinerR.Infrastructure.Services
{
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
            // Serialize the recipe object to JSON
            StringContent requestContent = new StringContent(
                JsonSerializer.Serialize(recipe),
                Encoding.UTF8,
                "application/json");

            // Send POST request to the API
            HttpResponseMessage response = await _httpClient.PostAsync(_endpointUrl, requestContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error occurred while calling recommender system: {response.ReasonPhrase}");
            }

            // Read and parse the response
            string responseContent = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON response into a list of objects matching the expected structure
            var wineRecommendations = JsonSerializer.Deserialize<List<WineRecommendation>>(responseContent);

            if (wineRecommendations == null)
            {
                throw new Exception("Failed to deserialize the recommender system response.");
            }

            // Filter the recommendations to get only the wines where is_pairing is true
            List<int> pairedWineIds = wineRecommendations
                .Where(r => r.IsPairing)               // Filter where is_pairing is true
                .SelectMany(r => r.WineId)             // Extract the wine_id array
                .ToList();                              // Convert to a list

            return pairedWineIds;
        }
    }
    
    public class WineRecommendation
    {
        public bool IsPairing { get; set; }
        public List<int> WineId { get; set; } = null!;
    }
}
