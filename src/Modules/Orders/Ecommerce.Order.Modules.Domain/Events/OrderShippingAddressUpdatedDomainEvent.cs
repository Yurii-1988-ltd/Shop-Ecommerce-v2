

namespace Ecommerce.Order.Modules.Domain.Events;

internal sealed class OrderShippingAddressUpdatedDomainEvent(Guid orderId): DomainEvent
{
    public Guid OrderId { get;  } = orderId;
}
