

namespace Ecommerce.Order.Modules.Domain.Events;

internal sealed class OrderItemAddedDomainEvent(Guid OrderId,
                                                Guid OrderItemId,
                                                Guid ProductId,
                                                int Quantity) : DomainEvent
{
}
