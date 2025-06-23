using marketplace_api.Models;
using marketplace_api.ModelsDto;

namespace marketplace_api.Common.interfaces;

public interface IShopRepository
{
  public Task<Shop> GetShop(Guid shopId);
  public Task<List<Shop>> GetActiveShops();
  public Task<List<Shop>> GetInActiveShops();
  public Task<Shop> GetMyShop(Guid sellerId);
  public Task<Shop> CreateShop(Shop shop);
}
