
using Ecommerce.Modules.Users.Application.Fiatures.GetUserRoles;
using Ecommerce.Modules.Users.Contracts.Dto;

namespace Ecommerce.Modules.Users.Presentation.EndPoints.Users;

internal sealed class GetUserRolesendpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "/users/{userId:guid}/roles",
            async (
                Guid userId,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    new GetRolesQuery(userId),
                    cancellationToken);

                return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : Results.NotFound(result.Error);
            })
            .WithTags(Tags.Users)
            .WithName("GetUserRoles")
            .Produces<IReadOnlyList<RoleResponse>>(
                StatusCodes.Status200OK)
            .Produces(
                StatusCodes.Status404NotFound);
    }
}
