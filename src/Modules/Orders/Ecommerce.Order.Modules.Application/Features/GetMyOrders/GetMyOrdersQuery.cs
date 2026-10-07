using Ecommerce.Application.Pagination;
using Ecommerce.Order.Modules.Application.Features.Responses;
using Ecommerce.Shared.Contracts.Orders;


namespace Ecommerce.Order.Modules.Application.Features.GetMyOrders;

public sealed record GetMyOrdersQuery(
    Guid CustomerId,
    int Page = 1,
    int PageSize = 20)
    : IQuery<PagedResult<OrderListResponse>>;