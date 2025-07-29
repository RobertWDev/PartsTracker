using PartsTracker.Shared.Application.Messaging;

namespace PartsTracker.Modules.Users.Application.Users.GetUser;

public sealed record GetUserQuery(Guid UserId) : IQuery<UserResponse>;
