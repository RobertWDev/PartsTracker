using PartsTracker.Shared.Domain;

namespace PartsTracker.Modules.Parts.Domain.Parts;

public sealed class PartCreatedDomainEvent(string partNumber) : DomainEvent
{
    public string PartNumber { get; } = partNumber;
}
