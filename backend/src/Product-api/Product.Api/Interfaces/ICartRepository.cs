using Products.Api.Models;

namespace Products_Api.Interfaces;

public interface ICartRepository
{
  public Task AddCartItem(CartItem cartItem);
  public Task<List<CartItem>> GetCartItems(Guid userId);
  public Task DeleteCartItem(Guid productId, Guid userId);
  public Task ClearCart(Guid userId);
  public Task CreateCart(Guid userId);
}
