

namespace Ecommerce.Inventory.Modules.Application.Features.ReserveStock;

internal sealed class ReserveStockCommandHandler :ICommandHandler<ReserveStockCommand>
{
    private readonly IInventoryRepository _repository;
    private IInventoryUnitOfWork _unitOfWork;
    public ReserveStockCommandHandler(IInventoryRepository repository, IInventoryUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;

    }
    public async Task<Result> Handle(ReserveStockCommand request, CancellationToken cancellationToken)
    {
        var inventory = await _repository.GetAsync(request.InventoryItemId, cancellationToken);
        if (inventory == null)
        {
            return InventoryErrors.NotFound(request.InventoryItemId);
        }
        var result = inventory.Reserve(request.Quantity);
        if (result.IsFailure)
        {
            return result;
        }
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }

}
