using marketplace_api.DomainEvents;

namespace marketplace_api.Models;

public class BaseEntity
{
  public Guid Id { get; init ; } = Guid.NewGuid();

  public readonly List<IDomainEvent> DomainEvents = new();

  public void AddDomainEvent(IDomainEvent eventItem)
  {
    DomainEvents.Add(eventItem);
  }

  public void RemoveDomainEvent(IDomainEvent eventItem)
  {
    DomainEvents.Remove(eventItem);
  }

  public void ClearDomainEvents()
  {
    DomainEvents.Clear();
  }
}
