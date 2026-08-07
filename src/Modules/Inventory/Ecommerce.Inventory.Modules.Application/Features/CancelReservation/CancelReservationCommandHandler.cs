using Ecommerce.Inventory.Modules.Application.Abstractions;

namespace Ecommerce.Inventory.Modules.Application.Features.CancelReservation;

internal sealed class CancelReservationCommandHandler(IInventoryRepository repository, Ecommerce.Application.Abstractions.IUnitOfWork unitOfWork) : ICommandHandler<CancelReservationCommand>
{
    public async Task<Result> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
    {
        var inventory = await repository.GetAsync(request.InventoryItemId, cancellationToken);

        if (inventory is null)
            return InventoryErrors.NotFound(request.InventoryItemId);

        var result = inventory.CancelReservation(request.Quantity);

        if (result.IsFailure)
            return result;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
