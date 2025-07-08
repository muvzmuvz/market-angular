using marketplace_api.DomainEvents;
using marketplace_api.DomainEvents.Events;
using Microsoft.AspNetCore.Identity;

namespace marketplace_api.Models;

public class UserIdentity : IdentityUser<Guid>, IHasDomainEvents
{
  public string FirstName { get; set; }
  public string imagePath { get; set; }
  public decimal ExpenseSummary { get; set; }

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

  public static UserIdentity CreateUser(
    string firstName
    , string email
    , string password)
  {
    var user = new UserIdentity
    {
      FirstName = firstName,
      Email = email,
      UserName = email,
      imagePath = "defaultImage",
      ExpenseSummary = 0
    };
    user.SecurityStamp = Guid.NewGuid().ToString();

    user.AddDomainEvent(new CreateUserEvent());
    
    return user;  
  }

  public void SetCreatedEventId()
  {
    var createEvent = _domainEvents
        .OfType<CreateUserEvent>()
        .FirstOrDefault();

    if (createEvent != null)
    {
      createEvent.SetUserId(this.Id);
    }
  }
}
