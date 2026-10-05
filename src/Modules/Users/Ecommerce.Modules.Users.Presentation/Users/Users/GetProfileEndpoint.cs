

using Ecommerce.Modules.Users.Contracts.Abstractions;
using System.Security.Claims;

namespace Ecommerce.Modules.Users.Presentation.Users.Users;

internal sealed class GetProfileEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/users/me", async (
     ClaimsPrincipal user,
     IUserService userService,
     CancellationToken cancellationToken) =>
        {
            var userIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(userIdClaim, out var userId))
                return Results.Unauthorized();

            var profile = await userService.GetProfileAsync(
                userId,
                cancellationToken);

            return profile is null
                ? Results.NotFound()
                : Results.Ok(profile);
        })
 .RequireAuthorization();
    }
}
