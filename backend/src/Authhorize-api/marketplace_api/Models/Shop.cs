using marketplace_api.DomainEvents.Events;
using marketplace_api.services;
using Microsoft.AspNetCore.Identity;

namespace marketplace_api.Models;

public class Shop : BaseEntity
{
  public string Name { get; set; }
  public bool   IsActive { get; set; } = false;
  public string PassportOwner { get; init; }
  public string INN { get; init; }
  public string ImageOwnerPath { get; init; }
  public string Description { get; set; }
  public Guid OwnerId { get; set; }
  public UserIdentity Owner { get; set; }
  public decimal Profit { get; set; }
  public ICollection<ShopSeller> Sellers { get; set; } = new List<ShopSeller>();

  public static Shop Create(
      string description
    , string name
    , UserIdentity owner
    , string passportOwner
    , string INN
    , string imageOwnerPath)
  {
    var shop = new Shop()
    {
      Description = description,
      Name = name,
      OwnerId = owner.Id,
      Owner = owner,
      PassportOwner = passportOwner,
      ImageOwnerPath = imageOwnerPath,
      INN = INN
    };

    var shopSeller = new ShopSeller()
    {
      ShopId = shop.Id,
      Shop = shop,
      SellerId = owner.Id,
      Seller = owner
    };
    shop.Sellers.Add(shopSeller);

    return shop;
  }

  public ShopSeller AddSeller(UserIdentity user)
  {
    var shopSeller = new ShopSeller()
    {
      ShopId = Id,
      Shop = this,
      SellerId = user.Id,
      Seller = user  
    };

    Sellers.Add(shopSeller);
    AddDomainEvent(new ShopAddUserEvent(Id, Owner.Id));

    return shopSeller;
  }

  public void Activate()
  {
    if (IsActive == false)
    {
      IsActive = true;
      AddDomainEvent(new ShopActivatedEvent(Id, Owner.Id));
    }
  }

  public void LeaveShop(ShopSeller seller)
  {
    if (seller.Seller.Id == Owner.Id)
    {
      throw new InvalidOperationException("Владелец не может покинуть магазин");
    }

    Sellers.Remove(seller);
  }
}
