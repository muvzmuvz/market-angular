using marketplace_api.Models;
using marketplace_api.ModelsDto;

namespace marketplace_api.Common.interfaces;

public interface IShopService
{
  public Task<ShopDto> GetShop(Guid shopId);
  public Task<List<ShopDto>> GetActiveShops();
  public Task<List<ShopDto>> GetInActiveShops();
  public Task<ShopDto> GetMyShop(Guid sellerId);
  public Task<ShopDto> CreateShop(ShopDtoRequest shop, Guid userId);
  public Task<ShopDto> ActivateTheShop(Guid shopId);
  public Task<ShopFullInformationDto> GetFullInformtionShop(Guid shopId);
}
