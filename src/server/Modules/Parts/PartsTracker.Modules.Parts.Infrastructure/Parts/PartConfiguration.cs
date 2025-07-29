using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartsTracker.Modules.Parts.Domain.Parts;

namespace PartsTracker.Modules.Parts.Infrastructure.Parts;
internal sealed class PartConfiguration : IEntityTypeConfiguration<Part>
{
    public void Configure(EntityTypeBuilder<Part> builder)
    {
        builder.HasKey(p => p.PartNumber);
    }
}
