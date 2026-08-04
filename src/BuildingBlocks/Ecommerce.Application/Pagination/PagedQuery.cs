using Ecommerce.Application.CQRS;

namespace Ecommerce.Application.Pagination;

public abstract record PagedQuery<TResponse>(
  int Page = 1,
  int PageSize = 20)
  : IQuery<PagedResult<TResponse>>;
