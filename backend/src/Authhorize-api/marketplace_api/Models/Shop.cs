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
  public ICollection<ShopSeller> Sellers { get; set; } = new List<ShopSeller>();

  public static Shop Create(
      string description
    , string name
    , DomainUser owner)
  {
    var shop = new Shop()
    {
      Description = description,
      Name = name,
      OwnerId = owner.Id,
      UserId = owner.IdentityId,
      Owner = owner
    };

    var shopOwner = new ShopSeller()
    {
      ShopId = shop.Id,
      Shop = shop,
      SellerId = owner.Id,
      Seller = owner
    };

    shop.Sellers.Add(shopOwner);

    return shop;
  }

  public void Activate()
  {
    if (IsActive == false)
    {
      IsActive = true;
      AddDomainEvent(new ShopActivatedEvent(Id, Owner.IdentityId));
    }
  }
}
