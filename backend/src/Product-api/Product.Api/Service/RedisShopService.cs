using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Products.Api.BackgroundServices.EventModels;
using Products.Api.Interfaces;
using Products.Api.ModelsDto;
using System.Text.Json;

namespace Products.Api.Service;

public class RedisShopService : IRedisShopService
{
  private readonly IDistributedCache _cache;
  private readonly ILogger<RedisShopService> _logger; 

  public RedisShopService(
      IDistributedCache cache
    , ILogger<RedisShopService> logger)
  {
    _cache = cache;
    _logger = logger;
  }

  public async Task CreateShop(ShopUpdateEvent @event)
  {
    var item = await _cache.GetStringAsync($"shop:{@event.ShopId}");
    _logger.LogInformation("Creating shop with ID: {ShopId}", @event.ShopId);
    if (item != null)
    {
      throw new Exception("Магазин с таким ID уже существует!");
    }

    var shopUsers = new ShopUsersDto();
    shopUsers.UsersIds.Add(@event.SellerId);

    _logger.LogInformation("Adding seller with ID: {SellerId} to shop: {ShopId}", @event.SellerId, @event.ShopId);
    await _cache.SetStringAsync(
      $"shop:{@event.ShopId}",
      JsonSerializer.Serialize(shopUsers),
      new DistributedCacheEntryOptions
      {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
      });
    _logger.LogInformation("Shop with ID: {ShopId} created successfully", @event.ShopId);
  }

  public async Task DeleteSeller(ShopUpdateEvent @event)
  {
    var shopUsers = await GetCache(@event.ShopId);
    shopUsers.UsersIds.Remove(@event.SellerId);

    await _cache.SetStringAsync(
      $"shop:{@event.ShopId}",
      JsonSerializer.Serialize(shopUsers),
      new DistributedCacheEntryOptions
      {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
      });
  }

  public async Task<bool> ShopSeller(Guid shopId, Guid sellerId)
  {
    var shopUsers = await GetCache(shopId);
    if (shopUsers.UsersIds.Contains(sellerId))
    {
      return true;
    }

    return false;
  }

  public async Task AddSeller(ShopUpdateEvent @event)
  {
    var shopUsers = await GetCache(@event.ShopId);

    if (shopUsers.UsersIds.Contains(@event.SellerId))
    {
      throw new Exception("Продавец уже добавлен в магазин!");
    }

    shopUsers.UsersIds.Add(@event.SellerId);
  }

  private async Task<ShopUsersDto> GetCache(Guid shopId)
  {
    var item = await _cache.GetStringAsync($"shop:{shopId}")
      ?? throw new Exception("Магазин не найден!");

    var shopUsers = JsonSerializer.Deserialize<ShopUsersDto>(item);

    return shopUsers;
  }
}
