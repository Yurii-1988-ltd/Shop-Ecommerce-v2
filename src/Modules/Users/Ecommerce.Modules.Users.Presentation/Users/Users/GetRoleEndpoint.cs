
using Ecommerce.Modules.Users.Application.Fiatures.GetRole;
using Ecommerce.Modules.Users.Application.Fiatures.GetUsers;

namespace Ecommerce.Modules.Users.Presentation.EndPoints.Users;

internal sealed class GetRoleEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/roles/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetRoleQuery(id));
            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(result.Error);
        })
        .WithTags(Tags.Roles);
    }
}
