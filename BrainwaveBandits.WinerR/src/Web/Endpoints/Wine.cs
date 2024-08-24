using System.Collections.ObjectModel;
using System.Net.Sockets;
using BrainwaveBandits.WinerR.Application.Common.Models;
using BrainwaveBandits.WinerR.Application.Wines.Commands.CreateWine;
using BrainwaveBandits.WinerR.Application.Wines.Commands.DeleteWine;
using BrainwaveBandits.WinerR.Application.Wines.Commands.UpdateWine;
using BrainwaveBandits.WinerR.Application.Wines.Queries.GetWineRecommendation;
using BrainwaveBandits.WinerR.Application.Wines.Queries.GetWinesWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace BrainwaveBandits.WinerR.Web.Endpoints;

public class Wines : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetWinesWithPagination)
            .MapGet(RecommendWine, "/recommend")
            .MapPost(CreateWineByVoice, "/voice")
            .MapPost(CreateWine)
            .MapPut(UpdateWine, "{id}")
            .MapDelete(DeleteWine, "{id}");
    }

    public Task<PaginatedList<WineBriefDto>> GetWinesWithPagination(ISender sender, [AsParameters] GetWinesWithPaginationQuery query)
    {
        return sender.Send(query);
    }

    public Task<Collection<WineBriefDto>> RecommendWine(ISender sender, [AsParameters] GetWineRecommendationQuery query)
    {
        return sender.Send(query);
    }

    public Task<int> CreateWine(ISender sender, CreateWineCommand command)
    {
        return sender.Send(command);
    }

    [IgnoreAntiforgeryToken]
    public async Task<int> CreateWineByVoice(ISender sender, [FromForm] IFormFile file)
    {

        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            var audioFileDto = new AudioFileDto
            {
                FileName = file.FileName,
                FileContent = memoryStream.ToArray(),
                ContentType = file.ContentType
            };

            var result = await sender.Send(new UploadAudioFileCommand(audioFileDto));

            if (result == "File uploaded successfully")
                return 1;

        }

        return 1;

        // Call Whisper pass Audio file

        // Call 
    }


    public async Task<IResult> UpdateWine(ISender sender, int id, UpdateWineCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteWine(ISender sender, int id)
    {
        await sender.Send(new DeleteWineCommand(id));
        return Results.NoContent();
    }
}
