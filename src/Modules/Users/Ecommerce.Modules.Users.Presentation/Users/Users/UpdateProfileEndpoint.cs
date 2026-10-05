
using Ecommerce.Modules.Users.Contracts.Abstractions;
using System.Security.Claims;

namespace Ecommerce.Modules.Users.Presentation.Users.Users;

internal sealed class UpdateProfileEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPut("/users/me", async (
            UpdateProfileRequest request,
            ClaimsPrincipal user,
            IUserService userService,
            CancellationToken cancellationToken) =>
        {
            var userValue = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if(!Guid.TryParse(userValue, out var userId))
            {
                return Results.Unauthorized();
            }
            var result = await userService.UpdateProfileAsync(userId,
                request.FirstName,
                request.LastName,
                request.Email, 
                request.PhoneNumber,
                cancellationToken);
            return result.IsSuccess ? Results.Ok() 
            : Results.BadRequest(result.Error);

        })
            .RequireAuthorization()
            .WithTags(Tags.Users)
            .WithName("UpdateProfile")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}
public sealed record UpdateProfileRequest(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber);
