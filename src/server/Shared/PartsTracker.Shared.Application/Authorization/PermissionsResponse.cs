﻿namespace PartsTracker.Shared.Application.Authorization;

public sealed record PermissionsResponse(Guid UserId, HashSet<string> Permissions);
