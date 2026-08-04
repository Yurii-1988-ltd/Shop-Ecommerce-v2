

using Ecommerce.Application.CQRS;
using Ecommerce.Application.Pagination;
using Ecommerce.Order.Modules.Application.Features.Responses;

namespace Ecommerce.Order.Modules.Application.Features.GetOrders;

public sealed record GetOrdersQuery(int Page, int PageSize) : PagedQuery<OrderListResponse>;

