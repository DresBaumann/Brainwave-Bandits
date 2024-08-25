using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrainwaveBandits.WinerR.Application.Common.Interfaces;
using BrainwaveBandits.WinerR.Domain.Entities;
using BrainwaveBandits.WinerR.Domain.Events;

namespace BrainwaveBandits.WinerR.Application.ImportedWines.Commands.CreateImportedWine;
public record CreateImportedWineCommand : IRequest<int>
{
    public required string WineId { get; init; }

    public required string WineName { get; init; }

    public required string WineryName { get; init; }

    public required string Vintages { get; init; }
}

public class CreateImportedWineCommandCommandHandler : IRequestHandler<CreateImportedWineCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateImportedWineCommandCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateImportedWineCommand request, CancellationToken cancellationToken)
    {
        var entity = new ImportedWine
        {
            WineID = request.WineId,
            WineName = request.WineName,
            WineryName = request.WineryName,
            Vintages = request.Vintages
        };

        entity.AddDomainEvent(new ImportedWineCreatedEvent(entity));

        if (!_context.ImportedWines.Any(x => x.WineID == entity.WineID))
        {
            _context.ImportedWines.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        return -1;
    }
}
