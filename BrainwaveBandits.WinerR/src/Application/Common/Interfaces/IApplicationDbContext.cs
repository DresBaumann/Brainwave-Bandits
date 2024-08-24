﻿using BrainwaveBandits.WinerR.Domain.Entities;

namespace BrainwaveBandits.WinerR.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<Wine> Wines { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
