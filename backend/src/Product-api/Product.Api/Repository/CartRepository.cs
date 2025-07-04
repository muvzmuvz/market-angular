using Products.Api.Models;
using Products_Api.Interfaces;

namespace Products_Api.Repository
{
  public class CartRepository : ICartRepository
  {
    public Task AddCartItem(CartItem cartItem)
    {
      throw new NotImplementedException();
    }
    public Task ClearCart(Guid userId)
    {
      throw new NotImplementedException();
    }
    public Task DeleteCartItem(Guid productId, Guid userId)
    {
      throw new NotImplementedException();
    }
    public Task<List<CartItem>> GetCartItems(Guid userId)
    {
      throw new NotImplementedException();
    }
    public Task CreateCart(Guid userId)
    {
      throw new NotImplementedException();
    }
  }
  {
  }
}
