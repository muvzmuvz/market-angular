using Products.Api.Models;

namespace Products_Api.Interfaces;

public interface ICartRepository
{
  public Task<Cart> GetCart(Guid cartId);
  public Task AddCartByCartId(CartItem cartItem, Guid cartId);
  public Task<List<CartItem>> GetCartByCartIdItems(Guid cartId);
  public Task AddCartByUserId(CartItem cartItem, Guid userId);
  public Task<List<CartItem>> GetCartByUserIdItems(Guid userId);
  public Task DeleteCartItemByCartId(Guid productId, Guid userId);
  public Task DeleteCartItemByUserId(Guid productId, Guid userId);
  public Task ClearByCartIdCart(Guid cartId);
  public Task ClearByUserIdCart(Guid userId);
  public Task<bool> CartExists(Guid cartId);
  public Task CreateCart(Cart cart);
}
