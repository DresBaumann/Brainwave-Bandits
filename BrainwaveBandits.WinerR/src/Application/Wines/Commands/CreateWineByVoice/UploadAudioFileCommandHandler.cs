using System.Net.Http.Headers;
using BrainwaveBandits.WinerR.Application.Wines.Commands.CreateWine;
using BrainwaveBandits.WinerR.Domain.Entities;
using BrainwaveBandits.WinerR.Domain.Events;
using Microsoft.EntityFrameworkCore;

public class UploadAudioFileCommandHandler : IRequestHandler<UploadAudioFileCommand, string>
{
    private readonly HttpClient _httpClient;

    public UploadAudioFileCommandHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> Handle(UploadAudioFileCommand request, CancellationToken cancellationToken)
    {
        if (request.File == null || request.File.FileContent == null || request.File.FileContent.Length == 0)
            return "No file uploaded";

        // Prepare the request content
        var content = new MultipartFormDataContent();
        var fileContent = new ByteArrayContent(request.File.FileContent);

        // Set the correct content type (adjust based on the actual file type)
        fileContent.Headers.ContentType = new MediaTypeHeaderValue("audio/wav"); // Set appropriate MIME type for audio file

        // Add the file to the multipart form data with the correct field name
        content.Add(fileContent, "audio_file", request.File.FileName); // Ensure "audio_file" matches the Flask server

        // Send the file to the Flask API
        var response = await _httpClient.PostAsync("http://localhost:5002/audiowines", content, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return "File uploaded successfully";
        }

        return $"Failed to upload file. Status Code: {response.StatusCode}";
    }
}
