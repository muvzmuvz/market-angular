using Microsoft.EntityFrameworkCore;
using Products.Api.Data;
using Products.Api.Models;
using Products_Api.Interfaces;

namespace Products_Api.Repository;

public class CartRepository : ICartRepository
{
  private readonly ProductDbContext _productDbContext;

  public CartRepository(ProductDbContext productDbContext)
  {
    _productDbContext = productDbContext;
  }

  public async Task AddCartByCartId(CartItem cartItem, Guid cartId)
  {
    var cart = await GetCartByCartId(cartId);

    var existingItem = cart.Items.FirstOrDefault(ci => ci.ProductId == cartItem.ProductId);

    if (existingItem != null)
    {
      existingItem.Quantity += cartItem.Quantity;
      _productDbContext.CartItems.Update(existingItem);
    }
    else
    {
      cartItem.CartId = cartId;
      await _productDbContext.CartItems.AddAsync(cartItem);
    }

    await _productDbContext.SaveChangesAsync();
  }

  public async Task AddCartByUserId(CartItem cartItem, Guid userId)
  {
    var cart = await GetCartByUserId(userId);

    var existingItem = cart.Items.FirstOrDefault(ci => ci.ProductId == cartItem.ProductId);

    if (existingItem != null)
    {
      existingItem.Quantity += cartItem.Quantity;
    }
    else
    {
      cart.Items.Add(cartItem);
    }
  }

  public async Task ClearByCartIdCart(Guid cartId)
  {
    var cart = await GetCartByCartId(cartId);

    cart.Items.Clear();
  }

  public async Task ClearByUserIdCart(Guid userId)
  {
    var cart = await GetCartByUserId(userId);

    cart.Items.Clear();
  }

  public async Task DeleteCartItemByCartId(Guid productId, Guid userId)
  {
    var cart = await GetCartByUserId(userId);

    var itemToRemove = cart.Items.FirstOrDefault(ci => ci.ProductId == productId)
      ?? throw new Exception("Item not found in the cart");

    cart.Items.Remove(itemToRemove);
  }

  public async Task DeleteCartItemByUserId(Guid productId, Guid userId)
  {
    var cart = await GetCartByUserId(userId);

    var itemToRemove = cart.Items.FirstOrDefault(ci => ci.ProductId == productId)
      ?? throw new Exception("Item not found in the cart");

    cart.Items.Remove(itemToRemove);
  }

  public async Task<List<CartItem>> GetCartByCartIdItems(Guid cartId)
  {
    var cart = await GetCartByCartId(cartId);

    return cart.Items.ToList();
  }

  public async Task<List<CartItem>> GetCartByUserIdItems(Guid userId)
  {
    var cart = await GetCartByUserId(userId);

    return cart.Items.ToList();
  }


  private async Task<Cart> GetCartByUserId(Guid userId)
  {
    return await _productDbContext.Carts
      .Include(c => c.Items)
      .FirstOrDefaultAsync(c => c.UserId == userId)
      ?? throw new Exception("Cart not found for the specified user");
  }

  private async Task<Cart> GetCartByCartId(Guid cartId)
  {
    return await _productDbContext.Carts
      .Include(c => c.Items)
      .FirstOrDefaultAsync(c => c.Id == cartId)
      ?? throw new Exception("Cart not found");
  }

  public async Task<bool> CartExists(Guid cartId)
  {
    return await _productDbContext.Carts.AnyAsync(c => c.Id == cartId);
  }

  public async Task<Cart> GetCart(Guid cartId)
  {
    return await _productDbContext.Carts
      .Include(c => c.Items)
      .FirstOrDefaultAsync(c => c.Id == cartId)
      ?? throw new Exception("Cart not found");
  }

  public async Task CreateCart(Cart cart)
  {
     await _productDbContext.Carts.AddAsync(cart);
  }
}
