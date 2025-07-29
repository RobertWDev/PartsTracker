using MediatR;
using PartsTracker.Modules.Users.Application.Users.GetUserPermissions;
using PartsTracker.Shared.Application.Authorization;
using PartsTracker.Shared.Domain;

namespace PartsTracker.Modules.Users.Infrastructure.Authorization;

internal sealed class PermissionService(ISender sender) : IPermissionService
{
    public async Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId)
    {
        return await sender.Send(new GetUserPermissionsQuery(identityId));
    }
}
