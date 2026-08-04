
using MediatR;

namespace Ecommerce.Domain.Domain;

public interface IDomainEvent : INotification
{
    Guid Id { get;  }
    DateTime OccurredOnUtc {  get; }
}
