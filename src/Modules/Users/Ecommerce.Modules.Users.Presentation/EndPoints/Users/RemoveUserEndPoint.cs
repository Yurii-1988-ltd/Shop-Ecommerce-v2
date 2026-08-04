using Ecommerce.Modules.Users.Application.Fiatures.RemoveUser;

namespace Ecommerce.Modules.Users.Presentation.EndPoints.Users
{
    internal class RemoveUserEndPoint
    {
        public void MapEndpoints(IEndpointRouteBuilder app)
        {
            app.MapDelete("/users/{id:guid}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new RemoveUserCommand(id));
                return result.IsSuccess
                    ? Results.NoContent()
                    : Results.NotFound(result.Error);
            })
            .WithTags(Tags.Users)
            .Produces<UserResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        }
    }
}
