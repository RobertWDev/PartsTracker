using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PartsTracker.Modules.Parts.Application.Parts.CreatePart;
using PartsTracker.Shared.Domain;
using PartsTracker.Shared.Presentation.Endpoints;
using PartsTracker.Shared.Presentation.Results;

namespace PartsTracker.Modules.Parts.Presentation.Parts;

public sealed class CreatePart : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/parts", async (Request request, ISender sender) =>
        {
            var command = new CreatePartCommand(
                request.PartNumber,
                request.Description,
                request.QuantityOnHand,
                request.LocationCode
            );

            Result<string> result = await sender.Send(command);

            return result.Match(() => Results.Created($"/api/parts/{result.Value}", result.Value), ApiResults.Problem);
        })
        //.RequireAuthorization(Permissions.CreatePart)
        .WithTags(Tags.Parts)
        .Produces<string>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status409Conflict)
        .ProducesProblem(StatusCodes.Status401Unauthorized);
    }

    internal sealed class Request
    {
        public string PartNumber { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int QuantityOnHand { get; set; }
        public string LocationCode { get; set; } = null!;
    }
}
