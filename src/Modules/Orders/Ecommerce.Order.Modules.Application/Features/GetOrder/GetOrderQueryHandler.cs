using Ecommerce.Modules.Users.Contracts.Abstractions;
using Ecommerce.Modules.Users.Contracts.Dto;
using Ecommerce.Order.Modules.Application.Features.Responses;

namespace Ecommerce.Order.Modules.Application.Features.GetOrder;

internal sealed class GetOrderQueryHandler(
    IOrderRepository orderRepository,
    IUserQueries userQueries)
    : IQueryHandler<GetOrderQuery, OrderResponse>
{
    public async Task<Result<OrderResponse>> Handle(
        GetOrderQuery request,
        CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

        if (order is null)
        {
            return Result.Failure<OrderResponse>(OrderErrors.NotFound(request.OrderId));
        }

        // 1. Проверка прав доступа
        if (order.CustomerId.HasValue)
        {
            if (order.CustomerId != request.CustomerId)
            {
                return Result.Failure<OrderResponse>(OrderErrors.AccessDenied);
            }
        }

        // 2. Запрашиваем пользователя только если CustomerId не null
        UserDto? user = null;

        if (order.CustomerId.HasValue)
        {
            // Передаем order.CustomerId.Value (тип Guid, а не Guid?)
            user = await userQueries.GetByIdAsync(order.CustomerId.Value, cancellationToken);
        }

        // 3. Передаем user в метод ToResponse
        return Result.Success(order.ToResponse(user));
    }
}