

namespace Ecommerce.Inventory.Modules.Application.Features.Responses;

public record InventoryAvailabilityResponse(
    Guid ProductId,
    int AvailableQuantity);
