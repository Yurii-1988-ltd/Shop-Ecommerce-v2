using Ecommerce.Domain.Domain;

namespace Ecommerce.Modules.Users.Domain.Events
{
    internal sealed class UserRemovedDomainEvent(Guid Id) : DomainEvent
    {
        public Guid UserId { get; set; } = Id;
    }
    
}
