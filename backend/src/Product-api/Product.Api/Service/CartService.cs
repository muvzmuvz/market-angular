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
  private readonly ILogger<CartService> _logger;

  public CartService(
      ICartRepository cartRepository
    , IUnitOfWork unitOfWork,
      IProductRepository productRepository
    , IMapper mapper,
      IHttpContextAccessor httpContextAccessor,
      ILogger<CartService> logger)
  {
    _cartRepository = cartRepository;
    _unitOfWork = unitOfWork;
    _productRepository = productRepository;
    _mapper = mapper;
    _httpContextAccessor = httpContextAccessor;
    _logger = logger;
  }

  public async Task AddCartByCartId(CartItemDto cartItemDto, Guid cartId)
  {
    _logger.LogInformation("Adding cart item by cart ID: {CartId}", cartId);
    var product = await _productRepository.GetProductByIdAsync(cartItemDto.ProductId)
      ?? throw new ArgumentException("Product not found");

    var cartItem = _mapper.Map<CartItem>(cartItemDto);
    cartItem.ProductId = product.Id;
    cartItem.Product = product;

    if (cartId == Guid.Empty)
    {
      _logger.LogInformation("Creating a new cart since cart ID is empty");
      var newCart = Cart.CreateCart();
      newCart.AddCartItem(cartItem);

      await _cartRepository.CreateCart(newCart);

      _logger.LogInformation("New cart created with ID: {CartId}", newCart.Id);
      _httpContextAccessor
        .HttpContext?
        .Response
        .Cookies
        .Append("session_token", newCart.Id.ToString());

      await _unitOfWork.commitChange();
      return;
    }
      
    _logger.LogInformation("Adding cart item to existing cart with ID: {CartId}", cartId);
    await _cartRepository.AddCartByCartId(cartItem, cartId);
    await _unitOfWork.commitChange();
  }

  public async Task AddCartByUserId(CartItemDto cartItemDto, Guid userId)
  {
    var product = await _productRepository.GetProductByIdAsync(cartItemDto.ProductId)
      ?? throw new ArgumentException("Product not found");

    var cartItem = _mapper.Map<CartItem>(cartItemDto);
    cartItem.ProductId = product.Id;
    cartItem.Product = product;

    await _cartRepository.AddCartByUserId(cartItem, userId);
    await _unitOfWork.commitChange();
  }

  public async Task ClearByCartIdCart(Guid cartId)
  {
    await _cartRepository.ClearByCartIdCart(cartId);
    _logger.LogInformation("Cleared cart items for cart ID: {CartId}", cartId);

    await _unitOfWork.commitChange();
  }

  public async Task ClearByUserIdCart(Guid userId)
  {
    await _cartRepository.ClearByUserIdCart(userId);
    _logger.LogInformation("Cleared cart items for user ID: {UserId}", userId);

    await _unitOfWork.commitChange();
  }

  public async Task DeleteCartItemByCartId(Guid productId, Guid cartId)
  {
    await _cartRepository.DeleteCartItemByCartId(productId, cartId);
    _logger.LogInformation("Deleted cart item with product ID: {ProductId} from cart ID: {CartId}", productId, cartId);

    await _unitOfWork.commitChange();
  }

  public async Task DeleteCartItemByUserId(Guid productId, Guid userId)
  {
    await _cartRepository.DeleteCartItemByUserId(productId, userId);

    await _unitOfWork.commitChange();
  }

  public async Task<List<CartItemDto>> GetCartByCartIdItems(Guid cartId)
  {
    _logger.LogInformation("Retrieving cart items for cart ID: {CartId}", cartId);
    if (cartId == Guid.Empty)
    {
      _logger.LogInformation("Cart ID is empty, creating a new cart and returning an empty list of items");
      var newCart = Cart.CreateCart();
      await _cartRepository.CreateCart(newCart);

      _httpContextAccessor.HttpContext?.Response.Cookies.Append("session_token", newCart.Id.ToString());
      _logger.LogInformation("New cart created with ID: {CartId}", newCart.Id);

      _logger.LogInformation("Committing changes to the unit of work");
      await _unitOfWork.commitChange();

      return new List<CartItemDto>();
    }

    _logger.LogInformation("Fetching cart items for existing cart ID: {CartId}", cartId);
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
