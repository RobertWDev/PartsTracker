using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PartsTracker.Modules.Parts.Application.Parts.DeletePart;
using PartsTracker.Shared.Domain;
using PartsTracker.Shared.Presentation.Endpoints;
using PartsTracker.Shared.Presentation.Results;

namespace PartsTracker.Modules.Parts.Presentation.Parts;
public sealed class DeletePart : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/parts/{partNumber}", async (string partNumber, ISender sender) =>
        {
            var command = new DeletePartCommand(partNumber);

            Result result = await sender.Send(command);

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        //.RequireAuthorization(Permissions.DeletePart)
        .WithTags(Tags.Parts)
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status401Unauthorized);
    }
}
