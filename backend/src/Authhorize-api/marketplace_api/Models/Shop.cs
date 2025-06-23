using marketplace_api.DomainEvents.Events;

namespace marketplace_api.Models;

public class Shop : BaseEntity
{
  public string Name { get; set; }
  public bool IsActive { get; set; } = false;
  public string Description { get; set; }
  public Guid OwnerId { get; set; }
  public DomainUser Owner { get; set; }
  public decimal Profit { get; set; }
  public Guid UserId { get; set; }
  public ICollection<ShopSeller> Sellers { get; set; }

  public void Activate()
  {
    if (IsActive == false)
    {
      IsActive = true;
      AddDomainEvent(new ShopActivatedEvent(Id, OwnerId));
    }
  }
}
