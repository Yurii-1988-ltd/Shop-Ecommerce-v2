namespace Ecommerce.Domain.Domain;

public abstract class DomainEvent : IDomainEvent
{
    public Guid Id {  get; }

    public DateTime OccurredOnUtc {  get; }

    protected DomainEvent()
    {
        Id = Guid.NewGuid();
        OccurredOnUtc = DateTime.UtcNow;
        
    }
}