

using Ecommerce.Modules.Users.Application.Fiatures.GetRoles;

namespace Ecommerce.Modules.Users.Presentation.EndPoints.Users;

internal sealed class GetRolesEndpoint 
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/roles", async (ISender sender, CancellationToken cancelationToken) =>
        {
            var result = await sender.Send(new GetRolesQuery(), cancelationToken);
            return result.Match(Results.Ok, Results.BadRequest);
        }).WithTags(Tags.Roles);
    }
}
