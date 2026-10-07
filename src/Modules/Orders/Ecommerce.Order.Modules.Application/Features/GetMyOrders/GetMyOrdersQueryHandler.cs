using Ecommerce.Application.Pagination;
using Ecommerce.Order.Modules.Application.Features.GetMyOrders;
using Ecommerce.Order.Modules.Domain.Enums;
using Ecommerce.Shared.Contracts.Orders;

internal sealed class GetMyOrdersQueryHandler(
    IOrderRepository repository)
    : IQueryHandler<GetMyOrdersQuery, PagedResult<OrderListResponse>>
{
    public async Task<Result<PagedResult<OrderListResponse>>> Handle(
        GetMyOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var result = await repository.GetPagedAsync(
            request.Page,
            request.PageSize,
            search: null,
            status: null,
            customerId: request.CustomerId,
            from: null,
            to: null,
            sortBy: OrderSortBy.CreateAt,
            descending: true,
            cancellationToken);

        var items = result.Items
            .Select(x => x.ToMyOrderListResponse())
            .ToList();

        return new PagedResult<OrderListResponse>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = result.TotalCount
        };
    }
}