

namespace Ecommerce.Cart.Modules.Infrastructure.Integrations;

internal sealed record InventoryAvailabilityResponse(Guid ProductId, int AvailableQuantity);

