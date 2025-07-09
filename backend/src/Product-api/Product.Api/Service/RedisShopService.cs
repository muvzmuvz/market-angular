using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Products.Api.BackgroundServices.EventModels;
using Products.Api.Interfaces;
using Products.Api.ModelsDto;
using Products_Api.Cron;
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

  public async Task UpdateShopCacheAsync(List<Shop> shops)
  {
    _logger.LogInformation("Updating shop cache for {ShopCount} shops", shops.Count);
    foreach (Shop shop in shops)
    {
      _logger.LogInformation("Processing shop with ID: {ShopId}", shop.Id);

      var cache = await _cache.GetStringAsync($"shop:{shop.Id}");
      if(cache == null)
      {
        var shopUsers = new ShopUsersDto();
        foreach (var seller in shop.Sellers)
        {
          shopUsers.UsersIds.Add(seller.SellerId);
        }

        _logger.BeginScope("Creating cache for shop with ID: {ShopId}", shop.Id);
        await _cache.SetStringAsync(
          $"shop:{shop.Id}",
          JsonSerializer.Serialize(shopUsers),
          new DistributedCacheEntryOptions
          {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
          });
      }
      else
      {
        _logger.BeginScope("Updating cache for shop with ID: {ShopId}", shop.Id);
        var shopUsers = JsonSerializer.Deserialize<ShopUsersDto>(cache);
        foreach (var seller in shop.Sellers)
        {
          if (!shopUsers.UsersIds.Contains(seller.SellerId))
          {

            _logger.LogInformation("Adding seller with ID: {SellerId} to shop: {ShopId}", seller, shop.Id);
            shopUsers.UsersIds.Add(seller.SellerId);
          }

          await _cache.SetStringAsync(
                   $"shop:{shop.Id}",
                   JsonSerializer.Serialize(shopUsers),
                   new DistributedCacheEntryOptions
                   {
                     AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(30)
                   });
        }
      }
    }
  }
}
