namespace marketplace_api.DomainEvents.Events;

public class CreateUserEvent : IDomainEvent
{
  public Guid UserId { get; set; }

  public void SetUserId(Guid userId)
  {
    UserId = userId;
  }
}
