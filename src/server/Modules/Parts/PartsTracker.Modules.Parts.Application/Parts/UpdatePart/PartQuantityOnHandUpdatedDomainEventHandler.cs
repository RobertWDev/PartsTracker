using PartsTracker.Modules.Parts.Domain.Parts;
using PartsTracker.Shared.Application.Messaging;

namespace PartsTracker.Modules.Parts.Application.Parts.UpdatePart;
internal sealed class PartQuantityOnHandUpdatedDomainEventHandler(IPartsRepository partsRepository) : DomainEventHandler<PartQuantityOnHandUpdatedDomainEvent>
{
    public override async Task Handle(PartQuantityOnHandUpdatedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        // Send a notification, log the event, or perform any other action needed when a part quantity is updated.
        Part? part = await partsRepository.GetAsync(domainEvent.PartNumber, cancellationToken);
        Console.WriteLine($"Part deleted: {domainEvent.PartNumber}, Description: {part!.Description}, Quantity: {part!.QuantityOnHand}, Location: {part!.LocationCode}, Last Stock Take: {part!.LastStockTake}");
    }
}
