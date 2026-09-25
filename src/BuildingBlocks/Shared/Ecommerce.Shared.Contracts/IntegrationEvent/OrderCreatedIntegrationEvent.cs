
namespace Ecommerce.Shared.Contracts.IntegrationEvent;

public sealed record OrderCreatedIntegrationEvent(
    Guid OrderId,
    Guid CustomerId,
    string CustomerEmail,
    decimal TotalAmount,
    string Currency,
    DateTime CreatedAtUtc
);
