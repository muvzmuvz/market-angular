namespace Products.Api.Models;

public class CartItem : BaseEntity
{
  private const int _defaultQuantity = 1;  

  public Guid ProductId { get; set; }
  public Product Product { get; set; }
  public int Quantity { get; set; } = _defaultQuantity;
  public Guid CartId { get; set; }
}
