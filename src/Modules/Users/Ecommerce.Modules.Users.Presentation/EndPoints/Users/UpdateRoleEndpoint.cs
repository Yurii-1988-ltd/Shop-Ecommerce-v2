using Ecommerce.Modules.Users.Application.Fiatures.UpdateRole;

namespace Ecommerce.Modules.Users.Presentation.EndPoints.Users;

internal sealed class UpdateRoleEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPut("/roles/{id:guid}", async (Guid id, UpdateRoleRequest request, ISender sender) =>
        {
            var result = await sender.Send(new UpdateRoleCommand(id, request.name));
            return result.IsSuccess
    ? Results.NoContent()
    : Results.NotFound(result.Error);
        })
        .WithTags(Tags.Roles)
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
}
public record UpdateRoleRequest(string name);
