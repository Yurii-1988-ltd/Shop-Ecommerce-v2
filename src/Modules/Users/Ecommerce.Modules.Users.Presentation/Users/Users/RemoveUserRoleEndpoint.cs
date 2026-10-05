using Ecommerce.Modules.Users.Application.Fiatures.RemoveUserRole;
using Ecommerce.Modules.Users.Presentation;

internal sealed class RemoveUserRoleEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(
            "/users/{userId:guid}/roles/{roleId:guid}",
            async (
                Guid userId,
                Guid roleId,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    new RemoveUserRoleCommand(userId, roleId),
                    cancellationToken);

                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }

                return Results.NoContent();
            })
            .WithTags(Tags.Roles)
            .WithName("RemoveUserRole")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest);
    }
}