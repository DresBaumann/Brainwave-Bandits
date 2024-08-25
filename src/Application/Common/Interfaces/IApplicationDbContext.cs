using BrainwaveBandits.WinerR.Domain.Entities;

namespace BrainwaveBandits.WinerR.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Wine> Wines { get; }

    DbSet<ImportedWine> ImportedWines { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
