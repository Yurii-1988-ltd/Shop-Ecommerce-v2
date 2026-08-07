

namespace Ecommerce.Inventory.Modules.Application.Features.Responses;

internal sealed record InventoryItemResponse(
    Guid InventoryItemId,
    Guid ProductId,
    string SKU,
    DateTime UpdatedUtc ,
    int OnHandQuantity ,
    int ReservedQuantity );

