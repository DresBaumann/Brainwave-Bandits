using BrainwaveBandits.WinerR.Application.Common.Interfaces;
using BrainwaveBandits.WinerR.Domain.Entities;
using BrainwaveBandits.WinerR.Domain.Events;

namespace BrainwaveBandits.WinerR.Application.Wines.Commands.CreateWine;

public record CreateWineCommand : IRequest<int>
{
    public required string WineID { get; init; }

    public required string Name { get; init; }

    public string? Brand { get; init; }

    public int Vintage { get; init; } = 0;

    public int Amount { get; init; } = 0;
}

public class CreateWineCommandCommandHandler : IRequestHandler<CreateWineCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateWineCommandCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateWineCommand request, CancellationToken cancellationToken)
    {
        var entity = new Wine
        {
            WineID = request.WineID,
            Name = request.Name,
            Brand = request.Brand,
            Vintage = request.Vintage,
            Amount = request.Amount,
            Done = false
        };

        entity.AddDomainEvent(new WineCreatedEvent(entity));

        _context.Wines.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
