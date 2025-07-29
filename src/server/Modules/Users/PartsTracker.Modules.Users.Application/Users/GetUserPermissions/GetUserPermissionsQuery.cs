using PartsTracker.Shared.Application.Authorization;
using PartsTracker.Shared.Application.Messaging;

namespace PartsTracker.Modules.Users.Application.Users.GetUserPermissions;

public sealed record GetUserPermissionsQuery(string IdentityId) : IQuery<PermissionsResponse>;
