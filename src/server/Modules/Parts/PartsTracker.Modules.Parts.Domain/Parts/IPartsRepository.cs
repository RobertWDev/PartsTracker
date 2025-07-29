namespace PartsTracker.Modules.Parts.Domain.Parts;
public interface IPartsRepository
{
    Task<Part?> GetAsync(string partNumber, CancellationToken cancellationToken = default);

    Task<bool> ExistsByPartNumberAsync(string partNumber, CancellationToken cancellationToken = default);

    void Insert(Part user);
    void Update(Part user);
}
