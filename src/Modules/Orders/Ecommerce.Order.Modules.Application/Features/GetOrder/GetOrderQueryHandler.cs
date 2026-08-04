using Ecommerce.Modules.Users.Contracts.Abstractions;
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
        var order = await orderRepository.GetByIdAsync(
            request.OrderId,
            cancellationToken);

        if (order is null)
        {
            return OrderErrors.NotFound(request.OrderId);
        }

        var user = await userQueries.GetByIdAsync(
            order.CustomerId,
            cancellationToken);

        return order.ToResponse(user);
    }
}