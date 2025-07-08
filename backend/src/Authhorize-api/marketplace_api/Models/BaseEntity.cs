  using marketplace_api.DomainEvents;

namespace marketplace_api.Models;

  public class BaseEntity  : IHasDomainEvents
  {
  public Guid Id { get; init; } = Guid.NewGuid();
  public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

  private readonly List<IDomainEvent> _domainEvents = new();

  public void AddDomainEvent(IDomainEvent eventItem)
  {
    _domainEvents.Add(eventItem);
  }

  public void RemoveDomainEvent(IDomainEvent eventItem)
  {
    _domainEvents.Remove(eventItem);
  }

  public void ClearDomainEvents()
  {
    _domainEvents.Clear();
  }
}
