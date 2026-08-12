namespace Ecommerce.Admin.ApiClients.Inventories.Responses;

public sealed record InventoryReportItemResponse(
 Guid InventoryItemId,
 Guid ProductId,
 string SKU,
 int OnHandQuantity,
 int ReservedQuantity,
 int AvailableQuantity,
 int MinimumQuantity);
