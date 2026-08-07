

namespace Ecommerce.Inventory.Modules.Application.Features.DeductStock;

internal sealed class DeductStockCommandHandler(IInventoryRepository repository) : ICommandHandler<DeductStockCommand>
{
    public async Task<Result> Handle(DeductStockCommand request, CancellationToken cancellationToken)
    {
       var inventory = await repository.GetAsync(request.InventoryItemId, cancellationToken);
        if (inventory == null)
        {
            return InventoryErrors.NotFound(request.InventoryItemId);
        }
        var result = inventory.Deduct(request.Quantity);
        if (result.IsFailure)
        {
            return result;
        }
        return Result.Success();
    }
}
