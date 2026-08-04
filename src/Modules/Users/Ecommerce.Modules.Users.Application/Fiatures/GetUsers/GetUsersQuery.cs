namespace Ecommerce.Modules.Users.Application.Fiatures.GetUsers;

public record GetUsersQuery() : IQuery<IReadOnlyList<UserResponse>>;

