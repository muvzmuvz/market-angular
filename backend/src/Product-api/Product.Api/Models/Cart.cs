namespace Products.Api.Models;

public class Cart : BaseEntity
{
  public Guid UserId { get; set; } = Guid.Empty;  
  public List<CartItem> Items { get; set; } = new();

  public static Cart CreateCart()
  {
    var cart = new Cart();

    return cart;
  }

  public void AddCartItem(CartItem cartItem)
  {
    if (cartItem == null)
    {
      throw new ArgumentNullException(nameof(cartItem), "Cart item cannot be null.");
    }
    if(Items.Any(i => i.ProductId == cartItem.ProductId))
    {
      var existingItem = Items.First(i => i.ProductId == cartItem.ProductId);
      existingItem.Quantity += cartItem.Quantity;
      return;
    }

    else
    {
      Items.Add(cartItem);
    }
  }

  public void AddUserId(Guid userId)
  {
    if(UserId == Guid.Empty)
    {
      UserId = userId;
    }
    else
    {
      throw new InvalidOperationException("UserId is already set.");
    }
  }
}
