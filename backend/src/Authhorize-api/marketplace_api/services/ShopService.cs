using AutoMapper;
using marketplace_api.Common.interfaces;
using marketplace_api.Exceptions;
using marketplace_api.Models;
using marketplace_api.ModelsDto;

namespace marketplace_api.services;

public class ShopService : IShopService
{
  private readonly IShopRepository _shopRepository;
  private readonly IMapper _mapper;
  private readonly IUserRepository _userRepository;
  private readonly IUnitOfWork _unitOfWork;

  public ShopService(IShopRepository shopRepository
    , IMapper mapper
    , IUserRepository userRepository,
      IUnitOfWork unitOfWork)
  {
    _shopRepository = shopRepository;
    _mapper = mapper;
    _userRepository = userRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<ShopDto> ActivateTheShop(Guid shopId, Guid userId)
  {
    var shop = await _shopRepository.ActivateTheShop(shopId);

    await _userRepository.UpdateRole(Role.Seller, userId);

    var shopDto = _mapper.Map<ShopDto>(shop);

    await _unitOfWork.commitChange();

    return shopDto;
  }

  public async Task<ShopDto> CreateShop(ShopDtoRequest shopDtoRequest)
  {
    if(shopDtoRequest == null)
      throw new ArgumentNullException(nameof(shopDtoRequest));

    var user = await _userRepository
      .GetUser(shopDtoRequest.UserId);

    var shop = new Shop()
    {
      Name = shopDtoRequest.Name,
      Description = shopDtoRequest.Description,
      Owner = user,
      OwnerId = user.Id,
      UserId = user.IdentityId,
    };
    await _shopRepository.CreateShop(shop);

    await _unitOfWork.commitChange();

    var shopDto = _mapper.Map<ShopDto>(_shopRepository);

    return shopDto;
  }

  public Task<List<ShopDto>> GetActiveShops()
  {
    throw new NotImplementedException();
  }

  public Task<List<ShopDto>> GetInActiveShops()
  {
    throw new NotImplementedException();
  }

  public Task<ShopDto> GetMyShop(Guid sellerId)
  {
    throw new NotImplementedException();
  }

  public Task<ShopDto> GetShop(Guid shopId)
  {
    throw new NotImplementedException();
  }
}
