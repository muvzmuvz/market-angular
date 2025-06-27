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
  private readonly ILogger<ShopService> _logger;
  private readonly IShopSellerRepository _shopSellerRepository;
  private readonly UserManager<UserIdentity> _userManager;
  private readonly IImageService _imageService;

  public ShopService(
      IShopRepository shopRepository
    , IMapper mapper,
      IUnitOfWork unitOfWork,
      ILogger<ShopService> logger,
      IShopSellerRepository shopSellerRepository,
      UserManager<UserIdentity> userManager,
      IImageService imageService)
  {
    _shopRepository = shopRepository;
    _mapper = mapper;
    _unitOfWork = unitOfWork;
    _logger = logger;
    _shopSellerRepository = shopSellerRepository;
    _userManager = userManager;
    _imageService = imageService;
  }

  public async Task<ShopDto> ActivateTheShop(Guid shopId)
  {
    _logger.LogInformation("Начало активации магазина");

    var shop = await _shopRepository.GetShop(shopId);

    shop.Activate();

    await _unitOfWork.commitChange();

    _logger.LogInformation("успешная активация магазина");

    var shopDto = _mapper.Map<ShopDto>(shop);

    return shopDto;
  }

  public async Task<ShopDto> CreateShop(ShopDtoRequest shopDtoRequest, Guid userId)
  {
    await ValidateNewSellerRequest(shopDtoRequest, userId);

    var user = await _userManager.FindByIdAsync(userId.ToString())
       ?? throw new UserNotFoundException("такой user  не найден");

    var imagePath = await _imageService.AploadImage(shopDtoRequest.OwnerImageBase64);

    var shop = Shop.Create(
        shopDtoRequest.Description
      , shopDtoRequest.Name
      , user
      , shopDtoRequest.PassportOwner
      , shopDtoRequest.INN
      , imagePath);
    shop.AddSeller(user); 

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

  public async Task<ShopFullInformationDto> GetFullInformtionShop(Guid shopId)
  {
    var shop = await _shopRepository.GetShop(shopId);

    var shopDto = _mapper.Map<ShopFullInformationDto>(shop);

    return shopDto;
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

  private async Task ValidateNewSellerRequest(ShopDtoRequest shopDtoRequest, Guid userId)
  {
    if (shopDtoRequest == null)
      throw new ArgumentNullException(nameof(shopDtoRequest));

    bool seller = await _shopSellerRepository.IsStoreMember(userId);
    if (seller)
      throw new Exception("вы уже состоите в магазине вам нельзя создавать");
  }
}
