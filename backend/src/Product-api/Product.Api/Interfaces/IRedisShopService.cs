using Products.Api.BackgroundServices.EventModels;

namespace Products.Api.Interfaces;

public interface IRedisShopService
{
  public Task CreateShop(ShopUpdateEvent @event);
  public Task AddSeller(ShopUpdateEvent @event);
  public Task DeleteSeller(ShopUpdateEvent @event);
  public Task<bool> ShopSeller(Guid shopId, Guid sellerId);
}
