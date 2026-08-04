namespace Ecommerce.Order.Modules.Domain.Events;
internal sealed class OrderStatusChangedDomainEvent(
    Guid orderId,
    OrderStatus oldStatus,
    OrderStatus newStatus) : DomainEvent
{
    public Guid OrderId { get; } = orderId;
    public OrderStatus OldStatus { get; } = oldStatus;
    public OrderStatus NewStatus { get; } = newStatus;
}