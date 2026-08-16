using Ecommerce.Application.Pagination;

namespace Ecommerce.Modules.Users.Application.Fiatures.GetUsers;

public record GetUsersQuery(int Page,int PageSize) : IQuery<PagedResult<UserResponse>>;

