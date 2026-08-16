using Ecommerce.Application.CQRS;
using Ecommerce.Application.Pagination;
using Ecommerce.Modules.Users.Application.Fiatures.Responses;


namespace Ecommerce.Modules.Users.Application.Fiatures.GetUser;

public record GetUserQuery(Guid Id) : IQuery<UserResponse>;

