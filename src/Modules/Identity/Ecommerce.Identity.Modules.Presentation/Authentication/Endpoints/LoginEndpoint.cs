using Ecommerce.Identity.Modules.Application.Features.Login;

namespace Ecommerce.Identity.Modules.Presentation.Authentication.Endpoints;

internal sealed class LoginEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app .MapPost("/login", async (LoginUserCommand command, ISender mediator) =>
             {
                 var result = await mediator.Send(command);
                 return result.IsSuccess
                     ? Results.Ok(result.Value)
                     : Results.BadRequest(result.Error);
             }).WithTags(Tags.Authentication);
    }
}
