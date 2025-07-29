using MediatR;
using PartsTracker.Modules.Users.Application.Users.GetUser;
using PartsTracker.Modules.Users.Domain.Users;
using PartsTracker.Modules.Users.IntegrationEvents;
using PartsTracker.Shared.Application.EventBus;
using PartsTracker.Shared.Application.Exceptions;
using PartsTracker.Shared.Application.Messaging;
using PartsTracker.Shared.Domain;

namespace PartsTracker.Modules.Users.Application.Users.RegisterUser;

internal sealed class UserRegisteredDomainEventHandler(ISender sender, IEventBus bus)
    : DomainEventHandler<UserRegisteredDomainEvent>
{
    public override async Task Handle(
        UserRegisteredDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Result<UserResponse> result = await sender.Send(
            new GetUserQuery(domainEvent.UserId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new PartsTrackerException(nameof(GetUserQuery), result.Error);
        }

        await bus.PublishAsync(
            new UserRegisteredIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                result.Value.Id,
                result.Value.Email,
                result.Value.FirstName,
                result.Value.LastName),
            cancellationToken);
    }
}
