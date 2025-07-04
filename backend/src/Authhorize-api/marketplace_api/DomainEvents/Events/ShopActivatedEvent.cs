namespace marketplace_api.DomainEvents.Events;

public class ShopActivatedEvent : IDomainEvent
{
  public Guid ShopId { get; set; }
  public Guid SellerId { get; set; }

  public ShopActivatedEvent(Guid shopId, Guid userId)
  {
    ShopId = shopId;
    SellerId = userId;
  }
}
