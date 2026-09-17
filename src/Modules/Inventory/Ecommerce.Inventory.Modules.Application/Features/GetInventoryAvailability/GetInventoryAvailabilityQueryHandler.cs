

using Ecommerce.Inventory.Modules.Application.Features.Responses;

namespace Ecommerce.Inventory.Modules.Application.Features.GetInventoryAvailability;

internal sealed class GetInventoryAvailabilityQueryHandler(IInventoryRepository inventoryRepository) : IQueryHandler<GetInventoryAvailabilityQuery, InventoryAvailabilityResponse>
{
    public async Task<Result<InventoryAvailabilityResponse>> Handle(GetInventoryAvailabilityQuery request, CancellationToken cancellationToken)
    {
        var inventory = await inventoryRepository.GetByProductByIdAsync(request.ProductId, cancellationToken);
        if (inventory is null) 
            return InventoryErrors.ProductInventoryNotFound(request.ProductId);
        return new InventoryAvailabilityResponse(inventory.ProductId, inventory.AvailableQuantity);
    }
}

