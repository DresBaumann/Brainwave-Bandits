using System.Net.Http.Json;

public class UploadAudioFileCommandHandler : IRequestHandler<UploadAudioFileCommand, List<string>>
{
    private readonly HttpClient _httpClient;

    public UploadAudioFileCommandHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<string>> Handle(UploadAudioFileCommand request, CancellationToken cancellationToken)
    {
        if (request.File == null || request.File.FileContent == null || request.File.FileContent.Length == 0)
            throw new ArgumentException("No file uploaded");

        // Prepare the raw audio content
        var fileContent = new ByteArrayContent(request.File.FileContent);

        // Set the content type to audio/wav
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("audio/wav");

        // Send the raw audio file directly in the body of the request
        var response = await _httpClient.PostAsync("http://localhost:5002/audiowines", fileContent, cancellationToken);

        // Ensure the request was successful
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Failed to upload file. Status Code: {response.StatusCode}");
        }

        // Deserialize the JSON response
        var responseJson = await response.Content.ReadFromJsonAsync<AudioWineResponse>(cancellationToken: cancellationToken);

        // Check if the response is valid
        if (responseJson == null || responseJson.MatchedWines == null)
        {
            throw new InvalidOperationException("Invalid response from the server.");
        }

        // Return matched wines as a list of strings with WineName and WineID
        var matchedWinesList = new List<string>();
        foreach (var wine in responseJson.MatchedWines)
        {
            matchedWinesList.Add($"{wine.WineName} - {wine.WineID}");
        }

        return matchedWinesList;
    }
}

// Define the corresponding models for deserialization
public class MatchedWine
{
    public string WineName { get; set; } = null!;
    public string WineID { get; set; } = null!;
}

public class AudioWineResponse
{
    public List<MatchedWine> MatchedWines { get; set; } = null!;
    public List<MatchedWine> NonMatchedWines { get; set; } = null!;
}
