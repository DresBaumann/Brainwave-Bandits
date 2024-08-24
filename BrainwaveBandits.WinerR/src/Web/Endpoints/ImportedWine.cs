using BrainwaveBandits.WinerR.Application.ImportedWines.Commands.ImportWines;
using BrainwaveBandits.WinerR.Web.Tasks.ImportWine;

namespace BrainwaveBandits.WinerR.Web.Endpoints;

public class ImportedWine : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(Import);
    }

    public Task<int> Import(ISender sender, ImportWinesCommand command)
    {
        return Task.FromResult(0);
    }
}
