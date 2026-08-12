namespace Ecommerce.Inventory.Modules.Application.Responses;

public sealed record InventoryReportItem(
    Guid InventoryItemId,
    Guid ProductId,
    string SKU,
    int OnHandQuantity,
    int ReservedQuantity,
    int AvailableQuantity,
    int MinimumQuantity);