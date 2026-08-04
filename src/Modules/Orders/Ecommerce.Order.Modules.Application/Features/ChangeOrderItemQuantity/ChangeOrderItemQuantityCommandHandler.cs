

namespace Ecommerce.Order.Modules.Application.Features.ChangeOrderItemQuantity;

internal sealed class ChangeOrderItemQuantityCommandHandler(IOrderRepository repository) 
                                                : ICommandHandler<ChangeOrderItemQuantityCommand>
{
    public async Task<Result> Handle(ChangeOrderItemQuantityCommand request, CancellationToken cancellationToken)
    {
        var order = await repository.GetByIdAsync(request.OrderId,cancellationToken);
        if (order is null)
        {
            return OrderErrors.NotFound(request.OrderId);
        }
        var result = order.ChangeItemQuantity(request.OrderItemId, request.Quantity);
        if (result.IsFailure)
        {
            return result;
        }
        return await repository.UpdateAsync(order, cancellationToken);
    }
}
