

namespace Ecommerce.Order.Modules.Application.Features.ChangeStatus;

internal sealed class ChangeOrderStatusCommandHandler(IOrderRepository repository) : ICommandHandler<ChangeOrderStatusCommand>
{
    public async Task<Result> Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await repository.GetByIdAsync(request.OrderId,cancellationToken);
        if (order is null)
        {
            return OrderErrors.NotFound(request.OrderId);
        }
        var result = order.ChangeStatus(request.OrderStatus);
        if(result.IsFailure)
        {
            return result;
        }
        return await repository.UpdateAsync(order,cancellationToken);
    }
}
