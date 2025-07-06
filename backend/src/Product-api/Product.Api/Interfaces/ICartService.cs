using Products.Api.Models;
using Products_Api.ModelsDto;

namespace Products_Api.Interfaces;

public interface ICartService
{
  public Task AddCartByCartId(CartItemDto cartItemDto, Guid cartId);
  public Task<List<CartItemDto>> GetCartByCartIdItems(Guid cartId);
  public Task AddCartByUserId(CartItemDto cartItemDto, Guid userId);
  public Task<List<CartItem>> GetCartByUserIdItems(Guid userId);
  public Task DeleteCartItemByCartId(Guid productId, Guid cartId);
  public Task DeleteCartItemByUserId(Guid productId, Guid userId);
  public Task ClearByCartIdCart(Guid cartId);
  public Task ClearByUserIdCart(Guid userId);
  public Task SetUserIdToCart(Guid userId);
}
