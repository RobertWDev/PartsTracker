using PartsTracker.Shared.Domain;

namespace PartsTracker.Modules.Parts.Domain.Parts;

public sealed class PartQuantityOnHandUpdatedDomainEvent(string partNumber, int quantityOnHand) : DomainEvent
{
    public string PartNumber { get; } = partNumber;
    public int QuantityOnHand { get; } = quantityOnHand;
}
