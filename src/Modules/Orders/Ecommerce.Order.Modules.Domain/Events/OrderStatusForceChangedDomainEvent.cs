
namespace Ecommerce.Order.Modules.Domain.Events;

internal sealed class OrderStatusForceChangedDomainEvent(Guid orderId, OrderStatus oldStatus, OrderStatus newStatus, Guid changedBy, string reason): DomainEvent
{
    public Guid OrderId { get; } = orderId;
    public OrderStatus OldStatus { get; } = oldStatus;
    public OrderStatus NewStatus { get; } = newStatus;
    public Guid ChangedBy { get; } = changedBy;
    public string Reason { get; } = reason;
}
