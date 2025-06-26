using AutoMapper;
using marketplace_api.Common.interfaces;
using marketplace_api.Exceptions;
using marketplace_api.Models;
using marketplace_api.ModelsDto;
using Microsoft.AspNetCore.Identity;

namespace marketplace_api.services;

public class ShopService : IShopService
{
  private readonly IShopRepository _shopRepository;
  private readonly IMapper _mapper;
  private readonly IUnitOfWork _unitOfWork;
  private readonly UserManager<UserIdentity> _userManager;

  public ShopService(
      IShopRepository shopRepository
    , IMapper mapper,
      IUnitOfWork unitOfWork
    , UserManager<UserIdentity> userManager)
  {
    _shopRepository = shopRepository;
    _mapper = mapper;
    _unitOfWork = unitOfWork;
    _userManager = userManager;
  }

  public async Task<ShopDto> ActivateTheShop(Guid shopId)
  {
    var shop = await _shopRepository.GetShop(shopId);

    shop.Activate();

    await _unitOfWork.commitChange();

    var shopDto = _mapper.Map<ShopDto>(shop);

    return shopDto;
  }

  public async Task<ShopDto> CreateShop(ShopDtoRequest shopDtoRequest)
  {
    if(shopDtoRequest == null)
      throw new ArgumentNullException(nameof(shopDtoRequest));

    var user = await _userManager.FindByIdAsync(shopDtoRequest.UserId.ToString())
       ?? throw new UserNotFoundException("такой user  не найден");

    var shop = Shop.Create(
        shopDtoRequest.Description
      , shopDtoRequest.Name
      , user);

    await _shopRepository.CreateShop(shop);

    await _unitOfWork.commitChange();

    var shopDto = _mapper.Map<ShopDto>(shop);

    return shopDto;
  }

  public async Task<List<ShopDto>> GetActiveShops()
  {
    var shops = await _shopRepository.GetActiveShops();

    var shopsDto = _mapper.Map<List<ShopDto>>(shops);

    return shopsDto;
  }

  public async Task<List<ShopDto>> GetInActiveShops()
  {
    var shops = await _shopRepository.GetInActiveShops();

    var shopsDto = _mapper.Map<List<ShopDto>>(shops);

    return shopsDto;
  }

  public async Task<ShopDto> GetMyShop(Guid sellerId)
  {
    var shop = await _shopRepository.GetMyShop(sellerId);

    var shopDto = _mapper.Map<ShopDto>(shop);

    return shopDto;
  }

  public async Task<ShopDto> GetShop(Guid shopId)
  {
    var shop = await _shopRepository.GetShop(shopId);

    var shopDto = _mapper.Map<ShopDto>(shop);

    return shopDto;
  }
}
