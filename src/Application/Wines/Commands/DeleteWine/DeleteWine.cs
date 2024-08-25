using BrainwaveBandits.WinerR.Application.Common.Interfaces;
using BrainwaveBandits.WinerR.Domain.Events;

namespace BrainwaveBandits.WinerR.Application.Wines.Commands.DeleteWine;

public record DeleteWineCommand(int Id) : IRequest;

public class DeleteWineCommandHandler : IRequestHandler<DeleteWineCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteWineCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteWineCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Wines
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Wines.Remove(entity);

        entity.AddDomainEvent(new WineDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }

}
