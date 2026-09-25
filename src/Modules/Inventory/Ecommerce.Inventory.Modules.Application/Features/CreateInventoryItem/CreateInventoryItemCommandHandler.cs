using Ecommerce.Inventory.Modules.Domain.Entities;

namespace Ecommerce.Inventory.Modules.Application.Features.CreateInventoryItem;

public sealed class CreateInventoryItemCommandHandler(IInventoryRepository repository, IInventoryUnitOfWork unitOfWork) : ICommandHandler<CreateInventoryItemCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
      CreateInventoryItemCommand request,
      CancellationToken cancellationToken)
    {
        var existing = await repository.GetByProductByIdAsync(
      request.ProductId,
      cancellationToken);

        if (existing is not null)
            return InventoryErrors.AlreadyExists;
        var result = InventoryItem.Create(
            request.ProductId,
            request.SKU,
            request.Quantity,
            request.MinimumQuantity);

        if (result.IsFailure)
            return result.Error;

        await repository.AddAsync(
            result.Value,
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(result.Value.Id);
    }
}
