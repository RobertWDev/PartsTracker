using PartsTracker.Shared.Application.Messaging;

namespace PartsTracker.Modules.Users.Application.Users.UpdateUser;

public sealed record UpdateUserCommand(Guid UserId, string FirstName, string LastName) : ICommand;
