namespace marketplace_api.DomainEvents;

public interface IHasDomainEvents
{
  IReadOnlyList<IDomainEvent> DomainEvents { get; }
  void AddDomainEvent(IDomainEvent eventItem);
  void RemoveDomainEvent(IDomainEvent eventItem);
  void ClearDomainEvents();
}
