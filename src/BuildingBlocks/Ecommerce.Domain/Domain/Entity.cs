

namespace Ecommerce.Domain.Domain;

public abstract class Entity
{
    private List<IDomainEvent> _domainEvents = new();
    public Guid Id { get; protected set; }
    public IReadOnlyCollection<IDomainEvent> DomainEvents
        => _domainEvents.AsReadOnly();

    public void ClearDomainEvent() => _domainEvents.Clear();

    protected void AddDomainEvent(IDomainEvent domainEvent)=>_domainEvents.Add(domainEvent);
    public void RemoveDomainEvent(IDomainEvent domainEvent)=> _domainEvents.Remove(domainEvent);
}
