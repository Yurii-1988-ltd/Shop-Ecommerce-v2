using Ecommerce.Modules.Users.Application.Fiatures.CreateRole;

namespace Ecommerce.Modules.Users.Presentation.EndPoints.Users;

internal sealed class CreateRoleEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/roles", async (
            CreateRoleRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new CreateRoleCommand(request.Name),
                cancellationToken);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.Ok(result.Value);
        })
        .WithTags(Tags.Roles)
        .WithName("CreateRole")
        .Produces<Guid>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
public record CreateRoleRequest(string Name);
