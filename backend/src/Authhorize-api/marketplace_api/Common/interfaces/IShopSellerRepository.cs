using marketplace_api.Models;

namespace marketplace_api.Common.interfaces;

public interface IShopSellerRepository
{
  public Task<ShopSeller> CreateShopSeller(ShopSeller shopSeller);

  public Task DeleteShopSeller(Guid shopSellerId);
}
