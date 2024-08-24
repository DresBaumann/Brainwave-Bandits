using System.Text.Json;
using BrainwaveBandits.WinerR.Application.Common.Interfaces;
using BrainwaveBandits.WinerR.Domain.Entities;
using BrainwaveBandits.WinerR.Infrastructure.Data.Configurations;
using Microsoft.Extensions.Options;
using OpenAI.Chat;

public class OpenAIService : IOpenAIService
{
    private readonly ChatClient _chatClient;

    public OpenAIService(IOptions<OpenAiOptions> options)
    {
        string apiKey = options.Value.ApiKey;
        
        _chatClient = new ChatClient("gpt-4", apiKey);
    }
    public async Task<Recipe> GetRecipeFromDishNameAsync(string dishName)
    {
        string prompt = $"Please provide the ingredients and main ingredient for the dish '{dishName}' in the following JSON format: " +
                        "{{ \"Name\": \"{dishName}\", \"Ingredients\": [ {{ \"Name\": \"ingredient1\" }}, {{ \"Name\": \"ingredient2\" }} ], \"MainIngredient\": {{ \"Name\": \"main ingredient\" }} }}.";

        // Send the prompt to OpenAI
        ChatCompletion completion = await _chatClient.CompleteChatAsync(new[]
        {
            new UserChatMessage(prompt),
        });

        // Ensure the result contains the expected data
        if (completion != null && completion.Content.Any())
        {
            var response = completion.Content[0].ToString();

            // If response is not null, attempt to clean and parse the JSON
            if (!string.IsNullOrEmpty(response))
            {
                try
                {
                    // Clean the response string to remove extra curly braces and trim whitespace
                    response = response.Trim().TrimStart('{').TrimEnd('}');
                    response = "{" + response + "}"; // Add a valid single curly brace for JSON

                    // Deserialize the cleaned JSON into a Recipe object
                    var recipe = JsonSerializer.Deserialize<Recipe>(response);

                    // Return the deserialized Recipe object
                    if (recipe != null)
                    {
                        return recipe;
                    }
                }
                catch (JsonException ex)
                {
                    throw new Exception("The model did not return valid JSON.", ex);
                }
            }
        }

        throw new Exception("Failed to retrieve or parse response from OpenAI.");
    }

}
