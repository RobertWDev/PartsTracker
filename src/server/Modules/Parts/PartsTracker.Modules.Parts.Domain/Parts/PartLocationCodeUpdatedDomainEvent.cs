using PartsTracker.Shared.Domain;

namespace PartsTracker.Modules.Parts.Domain.Parts;

public sealed class PartLocationCodeUpdatedDomainEvent(string partNumber, string locationCode) : DomainEvent
{
    public string PartNumber { get; } = partNumber;
    public string LocationCode { get; } = locationCode;
}
