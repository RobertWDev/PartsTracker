﻿using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PartsTracker.Modules.Users.Application.Users.UpdateUser;
using PartsTracker.Shared.Domain;
using PartsTracker.Shared.Infrastructure.Authentication;
using PartsTracker.Shared.Presentation.Endpoints;
using PartsTracker.Shared.Presentation.Results;

namespace PartsTracker.Modules.Users.Presentation.Users;

internal sealed class UpdateUserProfile : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("users/profile", async (Request request, ClaimsPrincipal claims, ISender sender) =>
        {
            Result result = await sender.Send(new UpdateUserCommand(
                claims.GetUserId(),
                request.FirstName,
                request.LastName));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.ModifyUser)
        .WithTags(Tags.Users);
    }

    internal sealed class Request
    {
        public string FirstName { get; init; }

        public string LastName { get; init; }
    }
}
