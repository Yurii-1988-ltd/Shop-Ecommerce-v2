

using Ecommerce.Modules.Users.Application.AssignRole;
using Ecommerce.Modules.Users.Application.Fiatures.CreateRole;

namespace Ecommerce.Modules.Users.Presentation.EndPoints.Users;

internal sealed class AssignRoleEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/users/{userId:guid}/roles/{roleId:guid}/assign", async (
            Guid userId,
            Guid roleId,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new AssignRoleCommand(userId,roleId),
                cancellationToken);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.Ok(result.Value);
        })
        .WithTags(Tags.Roles)
        .WithName("AssignRole")
        .Produces<Guid>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
