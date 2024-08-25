using BrainwaveBandits.WinerR.Application.Common.Interfaces;

namespace BrainwaveBandits.WinerR.Application.Wines.Commands.UpdateWine;

public record UpdateWineCommand : IRequest
{
    public int Id { get; init; }

    public required string WineID { get; init; }

    public required string Name { get; init; }

    public string? Brand { get; init; }

    public int Vintage { get; init; } = 0;

    public int Amount { get; init; } = 0;

    public bool Done { get; init; }
}

public class UpdateWineCommandHandler : IRequestHandler<UpdateWineCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateWineCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateWineCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Wines
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;
        entity.WineId = request.WineID;
        entity.Brand = request.Brand;
        entity.Vintage = request.Vintage;
        entity.Amount = request.Amount;
        entity.Done = request.Done;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
