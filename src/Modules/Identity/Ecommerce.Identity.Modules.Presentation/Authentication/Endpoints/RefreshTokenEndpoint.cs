

namespace Ecommerce.Identity.Modules.Presentation.Authentication.Endpoints;

internal sealed class RefreshTokenEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/refresh", async (
            RefreshTokenCommand command,
            ISender mediator) =>
        {
            var result = await mediator.Send(command);

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.BadRequest(result.Error);
        })
        .WithTags(Tags.Authentication);
    }
}
