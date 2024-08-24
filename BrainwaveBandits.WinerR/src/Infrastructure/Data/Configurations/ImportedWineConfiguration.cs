using BrainwaveBandits.WinerR.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrainwaveBandits.WinerR.Infrastructure.Data.Configurations;

public class ImportedWineConfiguration
{
    public void Configure(EntityTypeBuilder<ImportedWine> builder)
    {
        builder.Property(t => t.WineID)
            .IsRequired();
    }
}
