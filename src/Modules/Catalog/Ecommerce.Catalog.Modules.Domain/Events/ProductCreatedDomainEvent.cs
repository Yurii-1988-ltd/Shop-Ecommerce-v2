
using Ecommerce.Domain.Domain;

namespace Ecommerce.Catalog.Modules.Domain.Events;

internal sealed class ProductCreatedDomainEvent(Guid productId): DomainEvent
{
    public Guid Id { get; set; } = productId;
}
