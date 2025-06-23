namespace marketplace_api.DomainEvents.Events;

public class ShopActivatedEvent : IDomainEvent
{
  public Guid ShopId { get; set; }
  public Guid UserId { get; set; }

  public ShopActivatedEvent(Guid shopId, Guid userId)
  {
    ShopId = shopId;
    UserId = userId;
  }
}
