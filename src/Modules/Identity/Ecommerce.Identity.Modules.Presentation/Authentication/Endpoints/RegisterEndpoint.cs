using Ecommerce.Domain.Domain;
using Ecommerce.Identity.Modules.Application.Features.Register;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Ecommerce.Identity.Modules.Presentation.Authentication.Endpoints;

internal sealed class RegisterEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app
        .MapPost("/register", async (RegisterRequest request, ISender sender) =>
        {
            Result<AuthenticationResponse> result = await sender.Send(new RegisterUserCommand(
                  request.Email,
                  request.Password,
                  request.FirstName,
                  request.LastName


                ));
            return result.IsSuccess
     ? Results.Ok(result.Value)
     : Results.BadRequest(result.Error);
        })
            .WithTags(Tags.Authentication);
    }
}

internal record RegisterRequest(string Email, string Password, string FirstName, string LastName);