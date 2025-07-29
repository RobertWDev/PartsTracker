using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PartsTracker.Modules.Parts.Application.Parts.UpdatePart;
using PartsTracker.Shared.Domain;
using PartsTracker.Shared.Presentation.Endpoints;
using PartsTracker.Shared.Presentation.Results;

namespace PartsTracker.Modules.Parts.Presentation.Parts;
public sealed class UpdatePart : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/parts", async (Request request, ISender sender) =>
        {
            var command = new UpdatePartCommand(
                request.PartNumber,
                request.Description,
                request.QuantityOnHand,
                request.LocationCode,
                request.LastStockTake
            );

            Result result = await sender.Send(command);

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        //.RequireAuthorization(Permissions.UpdatePart)
        .WithTags(Tags.Parts)
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status404NotFound)
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
        public DateTime? LastStockTake { get; set; }
    }
}
