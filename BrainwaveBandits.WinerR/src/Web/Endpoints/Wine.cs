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

    public Task<int> CreateWineByVoice(ISender sender, [FromForm] IFormFile file)
    {
        var content = new MultipartFormDataContent(); 
        var fileContent = new StreamContent(file.OpenReadStream());

        // Call Whisper pass Audio file

        // Call 
        
        return Task.FromResult(1);
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
