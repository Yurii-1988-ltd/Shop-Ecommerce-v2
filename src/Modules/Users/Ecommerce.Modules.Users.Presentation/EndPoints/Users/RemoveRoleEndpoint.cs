using Ecommerce.Modules.Users.Application.Fiatures.RemoveRole;

namespace Ecommerce.Modules.Users.Presentation.EndPoints.Users;

internal sealed class RemoveRoleEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapDelete("/roles/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new RemoveRoleCommand(id));
            return result.IsSuccess
                ? Results.NoContent()
                : Results.NotFound(result.Error);
        })
        .WithTags(Tags.Roles)
        .Produces<UserResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

    }

}
