using marketplace_api.Common.interfaces;
using marketplace_api.Common.Persistence;
using marketplace_api.Models;
using Microsoft.EntityFrameworkCore;

namespace marketplace_api.repositories;

public class ShopSellerRepository : IShopSellerRepository
{
  private readonly AuthorizeDbContext _authorizeDbContext;

  public ShopSellerRepository(AuthorizeDbContext authorizeDbContext)
  {
    _authorizeDbContext = authorizeDbContext;
  }

  public async Task<ShopSeller> CreateShopSeller(ShopSeller shopSeller)
  {
    if(shopSeller == null)
      throw new ArgumentNullException(nameof(shopSeller));

    await _authorizeDbContext.AddAsync(shopSeller);

    return shopSeller;
  }

  public async Task DeleteShopSeller(Guid shopSellerId)
  {
    var shopSeller = await _authorizeDbContext.ShopSellers.FindAsync(shopSellerId)
       ?? throw new Exception("с таким id  нет участника в магазине");

    _authorizeDbContext.Remove(shopSeller.Id);
  }

  public async Task<bool> IsStoreMember(Guid userId)
  {
    var seller = await _authorizeDbContext.ShopSellers
      .FirstOrDefaultAsync(shopSeller => shopSeller.SellerId == userId);

    if(seller == null)
    {
      return false;
    }

    return true;
  }
}
