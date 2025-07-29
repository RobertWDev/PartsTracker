using Microsoft.EntityFrameworkCore;
using PartsTracker.Modules.Parts.Application.Abstractions.Data;
using PartsTracker.Modules.Parts.Domain.Parts;
using PartsTracker.Modules.Parts.Infrastructure.Parts;
using PartsTracker.Shared.Infrastructure.Inbox;
using PartsTracker.Shared.Infrastructure.Outbox;

namespace PartsTracker.Modules.Parts.Infrastructure.Database;

public sealed class PartsDbContext(DbContextOptions<PartsDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<Part> Parts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Parts);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new PartConfiguration());
    }
}
