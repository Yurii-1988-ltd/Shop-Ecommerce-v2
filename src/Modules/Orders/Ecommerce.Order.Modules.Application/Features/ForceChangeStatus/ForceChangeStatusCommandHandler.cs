

namespace Ecommerce.Order.Modules.Application.Features.ForceChangeStatus;

internal sealed class ForceChangeStatusCommandHandler(IOrderRepository repository) : ICommandHandler<ForceChangeStatusCommand>
{
    public async Task<Result> Handle(ForceChangeStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await repository.GetByIdAsync(request.OrderId, cancellationToken);
        if(order is null)
        {
            return OrderErrors.NotFound(request.OrderId);
        }
        var result = order.ForceChangeStatus(request.NewStatus, request.ChangedBy, request.Reason);
        if (result.IsFailure)
        {
            return result;
        }
      var updateResult = await repository.UpdateAsync(order, cancellationToken);
        if (updateResult.IsFailure)
        {
            return updateResult;
        }
        return Result.Success();


    }
}
