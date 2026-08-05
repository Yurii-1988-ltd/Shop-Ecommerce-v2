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
        // 1. Пробрасываем все параметры из запроса в репозиторий
        var (orders, totalCount) = await orderRepository.GetPagedAsync(
            request.Page,
            request.PageSize,
            request.Search,
            request.Status,
            request.CustomerId,
            request.From,
            request.To,
            request.SortBy,
            request.Descending,
            cancellationToken);

        if (orders.Count == 0)
        {
            return Result.Success(new PagedResult<OrderListResponse>
            {
                Items = [],
                Page = request.Page,
                PageSize = request.PageSize,
                TotalCount = totalCount
            });
        }

        // 2. Оптимизация N+1: параллельное или пакетное получение пользователей
        var customerIds = orders.Select(x => x.CustomerId).Distinct().ToList();

        // Запрашиваем всех уникальных пользователей параллельно
        var userTasks = customerIds.Select(id => userQueries.GetByIdAsync(id, cancellationToken));
        var usersList = await Task.WhenAll(userTasks);

        var usersDictionary = usersList
            .Where(u => u is not null)
            .ToDictionary(u => u!.Id);

        // 3. Формирование ответа
        var items = orders.Select(order =>
        {
            usersDictionary.TryGetValue(order.CustomerId, out var user);
            return order.ToListResponse(user);
        }).ToList();

        return Result.Success(new PagedResult<OrderListResponse>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        });
    }
}