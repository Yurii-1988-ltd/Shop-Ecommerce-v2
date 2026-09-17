namespace Ecommerce.Storefront.ApiClients.Inventories.Models;

public sealed record InventoryAvailabilityResponse(
    Guid ProductId,
    int AvailableQuantity
    );

