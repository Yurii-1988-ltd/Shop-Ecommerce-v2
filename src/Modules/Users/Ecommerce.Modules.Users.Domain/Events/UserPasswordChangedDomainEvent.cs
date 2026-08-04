
using Ecommerce.Domain.Domain;

namespace Ecommerce.Modules.Users.Domain.Events
{
    public sealed class UserPasswordChangedDomainEvent(Guid userId)
                                                        : DomainEvent
    {
        public Guid UserId { get;  } = userId;
      
    }
}
