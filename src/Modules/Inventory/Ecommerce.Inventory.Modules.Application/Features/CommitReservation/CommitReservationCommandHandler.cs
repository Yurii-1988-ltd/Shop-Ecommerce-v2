
namespace Ecommerce.Inventory.Modules.Application.Features.CommitReservation;

internal sealed class CommitReservationCommandHandler(IInventoryRepository repository,IUnitOfWork unitOfWork) : ICommandHandler<CommitReservationCommand>
{
    public async Task<Result> Handle(CommitReservationCommand request, CancellationToken cancellationToken)
    {
        var inventoryItem = await repository.GetAsync(request.inventoryItemId, cancellationToken);
        if(inventoryItem is null)
            return InventoryErrors.NotFound(request.inventoryItemId);
        inventoryItem.CommitReservation(request.Quantity);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
