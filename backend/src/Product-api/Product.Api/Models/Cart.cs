namespace Products.Api.Models;

public class Cart : BaseEntity
{
  public Guid UserId { get; set; }  
  public List<CartItem> Items { get; set; } = new();
  public CartStatus Status { get; set; } = CartStatus.Active;
}
