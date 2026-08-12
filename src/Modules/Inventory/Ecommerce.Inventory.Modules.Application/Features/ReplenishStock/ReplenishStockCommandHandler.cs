
namespace Ecommerce.Inventory.Modules.Application.Features.ReplenishStock;

internal sealed class ReplenishStockCommandHandler(IInventoryRepository repository, IUnitOfWork unitOfWork) : ICommandHandler<ReplenishStockCommand>
{
    public async Task<Result> Handle(ReplenishStockCommand request, CancellationToken cancellationToken)
    {
        var inventory = await repository.GetAsync(request.InventoryItemId, cancellationToken);
        if (inventory == null)
        {
            return InventoryErrors.NotFound(request.InventoryItemId);
        }
        var result = inventory.Replenish(request.Quantity);
        if (result.IsFailure)
        {
            return result;
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
