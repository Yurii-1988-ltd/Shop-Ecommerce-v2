using Ecommerce.Modules.Users.Application.Fiatures.UpdateUser;

namespace Ecommerce.Modules.Users.Presentation.EndPoints.Users;

internal sealed class UpdateUserEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPut("/users/{id:guid}", async (
            Guid id,
            UpdateUserRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                new UpdateUserCommand(
                    id,
                    request.FirstName,
                    request.LastName),
                cancellationToken);

            return result.IsSuccess
                ? Results.Ok()
                : Results.NotFound(result.Error);
        })
        .WithTags(Tags.Users)
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
internal sealed record UpdateUserRequest(string FirstName, string LastName);
