using AutoMapper;
using Products.Api.Interfaces;
using Products.Api.Models;
using Products_Api.Interfaces;
using Products_Api.ModelsDto;

namespace Products_Api.Service;

public class CartService : ICartService
{
  private readonly ICartRepository _cartRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IProductRepository _productRepository;
  private readonly IMapper _mapper;
  private readonly IHttpContextAccessor _httpContextAccessor;

  public CartService(
      ICartRepository cartRepository
    , IUnitOfWork unitOfWork,
      IProductRepository productRepository
    , IMapper mapper,
      IHttpContextAccessor httpContextAccessor)
  {
    _cartRepository = cartRepository;
    _unitOfWork = unitOfWork;
    _productRepository = productRepository;
    _mapper = mapper;
    _httpContextAccessor = httpContextAccessor;
  }

  public async Task AddCartByCartId(CartItemDto cartItemDto, Guid cartId)
  {
    if (cartId == Guid.Empty)
    {
      var newCart = Cart.CreateCart();

      var cartItems = _mapper.Map<CartItem>(cartItemDto);
      newCart.Items.Add(cartItems);

      await _cartRepository.CreateCart(newCart);

      _httpContextAccessor.HttpContext?.Response.Cookies.Append("session_token", newCart.Id.ToString());
    }

    var product = await _productRepository.GetProductByIdAsync(cartItemDto.ProductId)
      ?? throw new ArgumentException("Product not found");

    var cartItem = _mapper.Map<CartItem>(cartItemDto);

    await _cartRepository.AddCartByCartId(cartItem, cartId);
    await _unitOfWork.commitChange();
  }

  public async Task AddCartByUserId(CartItemDto cartItemDto, Guid userId)
  {
    var product = await _productRepository.GetProductByIdAsync(cartItemDto.ProductId)
      ?? throw new ArgumentException("Product not found");

    var cartItem = _mapper.Map<CartItem>(cartItemDto);

    await _cartRepository.AddCartByUserId(cartItem, userId);
    await _unitOfWork.commitChange();

  }

  public async Task ClearByCartIdCart(Guid cartId)
  {
    await _cartRepository.ClearByCartIdCart(cartId);
  }

  public async Task ClearByUserIdCart(Guid userId)
  {
    await _cartRepository.ClearByUserIdCart(userId);
  }

  public async Task DeleteCartItemByCartId(Guid productId, Guid cartId)
  {
    await _cartRepository.DeleteCartItemByCartId(productId, cartId);
  }

  public async Task DeleteCartItemByUserId(Guid productId, Guid userId)
  {
    await _cartRepository.DeleteCartItemByUserId(productId, userId);
  }

  public async Task<List<CartItemDto>> GetCartByCartIdItems(Guid cartId)
  {
    if (cartId == Guid.Empty)
    {
      var newCart = Cart.CreateCart();
      _httpContextAccessor.HttpContext?.Response.Cookies.Append("session_token", newCart.Id.ToString());

      return new List<CartItemDto>();
    }

    var cartItems = await _cartRepository.GetCartByCartIdItems(cartId);

    var cartItemDto = _mapper.Map<List<CartItemDto>>(cartItems);  

    return cartItemDto;
  }

  public async Task<List<CartItem>> GetCartByUserIdItems(Guid userId)
  {
    var cartItems = await _cartRepository.GetCartByUserIdItems(userId);

    return cartItems;
  }

  public Task SetUserIdToCart(Guid userId)
  {
    throw new NotImplementedException();
  }
}
