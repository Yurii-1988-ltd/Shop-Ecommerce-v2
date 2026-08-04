

namespace Ecommerce.Order.Modules.Application.Features.RemoveOrderItem;

internal sealed class RemoveOrderItemCommandHandler(IOrderRepository repository) : ICommandHandler<RemoveOrderItemCommand>
{
    public async Task<Result> Handle(RemoveOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await repository.GetByIdAsync(request.OrderId,cancellationToken);
        if (order is null)
            return OrderErrors.NotFound(request.OrderId);

        var result = order.RemoveOrderItem(request.OrderItemId);
        if (result.IsFailure)
            return result;
      var updateResult = await repository.UpdateAsync(order, cancellationToken);
        if(updateResult.IsFailure)
            return updateResult.Error;
        return Result.Success();
    }
}
