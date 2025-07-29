using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PartsTracker.Modules.Parts.Application.Parts.GetPart;
using PartsTracker.Modules.Parts.Application.Parts.GetParts;
using PartsTracker.Shared.Domain;
using PartsTracker.Shared.Presentation.Endpoints;
using PartsTracker.Shared.Presentation.Results;

namespace PartsTracker.Modules.Parts.Presentation.Parts;
public sealed class GetParts : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/parts", async (ISender sender) =>
        {
            var query = new GetPartsQuery();

            Result<IEnumerable<PartDto>> result = await sender.Send(query);

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        //.RequireAuthorization(Permissions.GetParts)
        .WithTags(Tags.Parts)
        .Produces<IEnumerable<PartDto>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status401Unauthorized);

        app.MapGet("/api/parts/{partNumber}", async (string partNumber, ISender sender) =>
        {
            var query = new GetPartQuery(partNumber);

            Result<PartDto?> result = await sender.Send(query);

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        //.RequireAuthorization(Permissions.GetParts)
        .WithTags(Tags.Parts)
        .Produces<PartDto>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status401Unauthorized);
    }
}
