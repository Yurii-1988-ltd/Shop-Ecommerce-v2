using Ecommerce.Application.Pagination;

namespace Ecommerce.Modules.Users.Application.Fiatures.GetUsers;

public record GetUsersQuery(int Page,
    int PageSize,
    string? Search = null) : IQuery<PagedResult<UserResponse>>;

