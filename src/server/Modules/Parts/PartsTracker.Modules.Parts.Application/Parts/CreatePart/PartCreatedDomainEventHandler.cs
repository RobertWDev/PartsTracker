using PartsTracker.Modules.Parts.Domain.Parts;
using PartsTracker.Shared.Application.Messaging;

namespace PartsTracker.Modules.Parts.Application.Parts.CreatePart;

internal sealed class PartCreatedDomainEventHandler(IPartsRepository partsRepository) : DomainEventHandler<PartCreatedDomainEvent>
{
    public override async Task Handle(PartCreatedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        // Send a notification, log the event, or perform any other action needed when a part is created.
        Part? part = await partsRepository.GetAsync(domainEvent.PartNumber, cancellationToken);
        Console.WriteLine($"Part created: {domainEvent.PartNumber}, Description: {part!.Description}, Quantity: {part!.QuantityOnHand}, Location: {part!.LocationCode}, Last Stock Take: {part!.LastStockTake}");
    }
}
