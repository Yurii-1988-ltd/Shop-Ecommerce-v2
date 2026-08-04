

using Ecommerce.Domain.Domain;

namespace Ecommerce.Modules.Users.Domain.Events
{
    internal sealed class CreatedUserDomainEvent(Guid UserId): DomainEvent
    {
        public Guid Id { get; set; } = UserId;
    }
}
