

using Ecommerce.Inventory.Modules.Application.Features.Responses;
using Ecommerce.Inventory.Modules.Domain.Entities;

namespace Ecommerce.Inventory.Modules.Application.Mappings;

internal static class InventoryMapper
{
    public static InventoryItemResponse ToResponse(this InventoryItem inventoryItem)
    {
        return new InventoryItemResponse(
            inventoryItem.Id,
            inventoryItem.ProductId,
            inventoryItem.SKU,
            inventoryItem.UpdatedUtc,
            inventoryItem.OnHandQuantity,
            inventoryItem.ReservedQuantity);
    }
}
