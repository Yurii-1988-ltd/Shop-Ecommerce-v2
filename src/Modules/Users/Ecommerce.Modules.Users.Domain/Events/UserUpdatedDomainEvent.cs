using Ecommerce.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Modules.Users.Domain.Events
{
    internal sealed class UserUpdatedDomainEvent(Guid UserId) : DomainEvent
    {
        public Guid Id { get; set; } = UserId;
    }
}
