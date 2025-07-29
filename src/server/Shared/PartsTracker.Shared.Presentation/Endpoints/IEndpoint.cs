using Microsoft.AspNetCore.Routing;

namespace PartsTracker.Shared.Presentation.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
