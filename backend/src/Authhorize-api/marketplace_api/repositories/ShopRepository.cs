using marketplace_api.Common.interfaces;
using marketplace_api.Common.Persistence;
using marketplace_api.Exceptions;
using marketplace_api.Models;
using Microsoft.EntityFrameworkCore;

namespace marketplace_api.repositories;

public class ShopRepository : IShopRepository
{
  private readonly AuthorizeDbContext _context;

  public ShopRepository(AuthorizeDbContext context)
  {
    _context = context;
  }

  public async Task<Shop> CreateShop(Shop shop)
  {
    await _context.AddAsync(shop);

    return shop;
  }

  public async Task<List<Shop>> GetActiveShops()
  {
    var shops = await _context.Shops
      .Include(shop => shop.Sellers)
      .Where(shp => shp.IsActive == true)
      .ToListAsync();

    return shops;
  }

  public async Task<List<Shop>> GetInActiveShops()
  {
    var shops = await _context.Shops
      .Include(shop => shop.Sellers)
      .Where(shp => shp.IsActive == false)
      .ToListAsync();

    return shops;
  }

  public async Task<Shop> GetMyShop(Guid sellerId)
  {
    var shop = await _context.Shops
      .Include(shop => shop.Sellers)
      .FirstOrDefaultAsync(shp => shp.OwnerId == sellerId)
        ?? throw new ShopNotFoundException($" не был найдет магазин Для пользователя с таким id: {nameof(sellerId)}");

    return shop;
  }

  public async Task<Shop> GetShop(Guid shopId)
  {
    var shop = await _context.Shops
      .Include(shop => shop.Sellers)
      .FirstOrDefaultAsync(shop => shop.Id == shopId)
        ?? throw new ShopNotFoundException($" не был найдет магазин с таким Id: {nameof(shopId)}");
    return shop;
  }
}
