using Ecommerce.Modules.Users.Application.Fiatures.GetUsers;

namespace Ecommerce.Modules.Users.Presentation.EndPoints.Users;

internal sealed class GetUsersEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/users", async (
            int page,
            int pageSize,
            string? search ,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new GetUsersQuery(page, pageSize,search),
                cancellationToken);

            return result.IsSuccess
                ? Results.Ok(result.Value)
                : Results.NotFound(result.Error);
        })
        .WithTags(Tags.Users);
    }
}