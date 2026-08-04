

using Ecommerce.Order.Modules.Domain.Enums;

namespace Ecommerce.Order.Modules.Application.Features.CancelOrder
{
    internal class CancelOrderCommandHandler(IOrderRepository repository) : ICommandHandler<CancelOrderCommand>
    {
        public async Task<Result> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await repository.GetByIdAsync(request.OrderId);
            if (order is null)
            {
                return OrderErrors.NotFound(request.OrderId);
            }
            var orderStatus = order.Cancel();
            if (orderStatus.IsFailure)
                return orderStatus;
            return await repository.UpdateAsync(order, cancellationToken);
           


        }
    }
}
