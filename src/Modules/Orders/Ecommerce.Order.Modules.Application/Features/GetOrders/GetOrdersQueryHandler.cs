using Ecommerce.Application.Pagination;
using Ecommerce.Modules.Users.Contracts.Abstractions;
using Ecommerce.Order.Modules.Application.Features.Responses;

namespace Ecommerce.Order.Modules.Application.Features.GetOrders;

internal sealed class GetOrdersQueryHandler(
    IOrderRepository orderRepository,
    IUserQueries userQueries)
    : IQueryHandler<GetOrdersQuery, PagedResult<OrderListResponse>>
{
    public async Task<Result<PagedResult<OrderListResponse>>> Handle(
        GetOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var (orders, totalCount) = await orderRepository.GetPagedAsync(
            request.Page,
            request.PageSize,
            cancellationToken);

        var items = new List<OrderListResponse>();

        foreach (var order in orders)
        {
            var user = await userQueries.GetByIdAsync(
                order.CustomerId,
                cancellationToken);

            items.Add(order.ToListResponse(user));
        }

        return Result.Success(new PagedResult<OrderListResponse>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        });
    }
}