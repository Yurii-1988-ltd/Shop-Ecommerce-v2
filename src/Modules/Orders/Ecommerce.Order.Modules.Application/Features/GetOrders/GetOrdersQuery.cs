using Ecommerce.Application.Pagination;
using Ecommerce.Order.Modules.Application.Features.Responses;
using Ecommerce.Order.Modules.Domain.Enums;

namespace Ecommerce.Order.Modules.Application.Features.GetOrders;

public sealed record GetOrdersQuery(int Page, int PageSize,
   string?Search,
   OrderStatus? Status,
   Guid? CustomerId,
   DateTime?From,
   DateTime?To,
   OrderSortBy SortBy= OrderSortBy.CreateAt,
   bool Descending = true) : PagedQuery<OrderListResponse>;

