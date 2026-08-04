using Ecommerce.Identity.Modules.Application.Features.ForgotPassword;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Ecommerce.Identity.Modules.Presentation.Authentication.Endpoints;

internal sealed class ForgotPasswordEndpoint
{
    public void MapEndPoints(IEndpointRouteBuilder app)
    {
        app.MapGroup("/auth")
            .MapPost("/forgot-password", async (ForgotPasswordCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);
                return result.IsSuccess
                ? Results.Ok()
                : Results.BadRequest(result.Error);
            }).WithTags(Tags.Authentication);
    }
}
