using BrainwaveBandits.WinerR.Application.Common.Models;
using BrainwaveBandits.WinerR.Application.Wines.Commands.CreateWine;
using BrainwaveBandits.WinerR.Application.Wines.Commands.DeleteWine;
using BrainwaveBandits.WinerR.Application.Wines.Commands.UpdateWine;
using BrainwaveBandits.WinerR.Application.Wines.Queries.GetWinesWithPagination;

namespace BrainwaveBandits.WinerR.Web.Endpoints;

public class Wines : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetWinesWithPagination)
            .MapPost(CreateWine)
            .MapPut(UpdateWine, "{id}")
            .MapDelete(DeleteWine, "{id}");
    }

    public Task<PaginatedList<WineBriefDto>> GetWinesWithPagination(ISender sender, [AsParameters] GetWinesWithPaginationQuery query)
    {
        return sender.Send(query);
    }

    public Task<int> CreateWine(ISender sender, CreateWineCommand command)
    {
        return sender.Send(command);
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
