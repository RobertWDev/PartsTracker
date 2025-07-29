using Microsoft.EntityFrameworkCore;
using PartsTracker.Modules.Parts.Domain.Parts;
using PartsTracker.Modules.Parts.Infrastructure.Database;

namespace PartsTracker.Modules.Parts.Infrastructure.Parts;
internal sealed class PartsRepository(PartsDbContext context) : IPartsRepository
{
    public async Task<bool> ExistsByPartNumberAsync(string partNumber, CancellationToken cancellationToken = default)
    {
        return await context.Parts.AnyAsync(p => !p.IsDeleted && p.PartNumber == partNumber, cancellationToken);
    }

    public Task<Part?> GetAsync(string partNumber, CancellationToken cancellationToken = default)
    {
        return context.Parts.FirstOrDefaultAsync(p => !p.IsDeleted && p.PartNumber == partNumber, cancellationToken);
    }

    public void Insert(Part user)
    {
        context.Parts.Add(user);
    }

    public void Update(Part user)
    {
        context.Parts.Update(user);
    }
}
