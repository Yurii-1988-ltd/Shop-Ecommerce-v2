using Ecommerce.Modules.Users.Application.Fiatures.CreateUser;

namespace Ecommerce.Modules.Users.Presentation.EndPoints.Users;

internal sealed class CreateUserEndpoint
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/users", async (UserRequest request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreateUserCommand(
                  request.Email,
                    request.FirstName,
                    request.LastName,
                    request.PasswordHash
                ));

            return result.IsSuccess
                ? Results.Created($"/users/{result.Value}", result.Value)
                : Results.BadRequest(result.Error);
        })
  .WithTags(Tags.Users)
  .Produces<UserResponse>(StatusCodes.Status201Created)
   .Produces(StatusCodes.Status404NotFound); ;
    }
}
internal sealed class UserRequest
{
    public string Email { get;  set; }
    public string FirstName { get;  set; }
    public string LastName { get;  set; }
    public string PasswordHash { get;  set; }


}
