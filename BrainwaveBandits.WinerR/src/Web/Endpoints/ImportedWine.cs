using BrainwaveBandits.WinerR.Application.ImportedWines.Queries.SearchImportedWines;

namespace BrainwaveBandits.WinerR.Web.Endpoints;

public class ImportedWine : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(Search);
    }

    public Task<IEnumerable<ImportedWineSearchResultDto>> Search(ISender sender, [AsParameters] SearchImportedWinesQuery query)
    {
        return sender.Send(query);
    }
}
