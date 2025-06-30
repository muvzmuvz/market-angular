namespace marketplace_api.DomainEvents.Events;

public class ShopAddUserEvent : IDomainEvent
{
  public Guid ShopId { get; set; }
  public Guid UserId { get; set; }

  public ShopAddUserEvent(Guid shopId, Guid userId)
  {
    ShopId = shopId;
    UserId = userId;
  }
}
