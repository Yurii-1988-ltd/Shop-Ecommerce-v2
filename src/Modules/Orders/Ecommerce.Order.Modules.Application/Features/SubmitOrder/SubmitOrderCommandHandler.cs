
namespace Ecommerce.Order.Modules.Application.Features.SubmitOrder;

internal sealed class SubmitOrderCommandHandler(IOrderRepository repository) : ICommandHandler<SubmitOrderCommand>
{
    public async Task<Result> Handle(SubmitOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await repository.GetByIdAsync(request.OrderId);
        if (order is null)
        {
            return OrderErrors.NotFound(request.OrderId);
        }
        var orderResult = order.Submit();
        if(orderResult.IsFailure)
        {
            return orderResult;
        }
       return await repository.UpdateAsync(order, cancellationToken);

    }
}
