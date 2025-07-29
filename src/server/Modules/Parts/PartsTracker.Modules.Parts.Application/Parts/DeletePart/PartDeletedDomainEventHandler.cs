using PartsTracker.Modules.Parts.Domain.Parts;
using PartsTracker.Shared.Application.Messaging;

namespace PartsTracker.Modules.Parts.Application.Parts.DeletePart;
internal sealed class PartDeletedDomainEventHandler(IPartsRepository partsRepository) : DomainEventHandler<PartDeletedDomainEvent>
{
    public override async Task Handle(PartDeletedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        // Send a notification, log the event, or perform any other action needed when a part is deleted.
        Part? part = await partsRepository.GetAsync(domainEvent.PartNumber, cancellationToken);
        Console.WriteLine($"Part deleted: {domainEvent.PartNumber}, Description: {part!.Description}, Quantity: {part!.QuantityOnHand}, Location: {part!.LocationCode}, Last Stock Take: {part!.LastStockTake}");
    }
}
