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
