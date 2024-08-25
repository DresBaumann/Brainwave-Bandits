using BrainwaveBandits.WinerR.Domain.Entities;

namespace BrainwaveBandits.WinerR.Application.Wines.Queries.GetWinesWithPagination;

public class WineBriefDto
{
    public int Id { get; init; }

    public string WineId { get; init; } = null!;

    public required string Name { get; init; }

    public string? Brand { get; init; }
    
    public int Vintage { get; init; }

    public int Amount { get; init; }

    public bool Done { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Wine, WineBriefDto>();
        }
    }
}
