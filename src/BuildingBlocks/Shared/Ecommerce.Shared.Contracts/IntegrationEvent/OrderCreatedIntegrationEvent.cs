
namespace Ecommerce.Shared.Contracts.IntegrationEvent;

public sealed record OrderCreatedIntegrationEvent(
    Guid OrderId,
    string OrderNumber,
    Guid CustomerId,
    string CustomerEmail,
    decimal TotalAmount,
    string Currency,
    IReadOnlyCollection<OrderCreatedItemIntegrationEvent>Items,
    DateTime CreatedAtUtc
);
