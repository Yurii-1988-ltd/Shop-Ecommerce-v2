

namespace Ecommerce.Shared.Contracts.IntegrationEvent;

public sealed record OrderCreatedItemIntegrationEvent(
    Guid ProductId,
    string ProductName,
    string Sku,
    decimal UnitPrice,
    int Quantity);

