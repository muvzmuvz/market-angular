namespace Products.Api.Models;

public class CartItem : BaseEntity
{
  public Guid ProductId { get; set; }
  public Product Product { get; set; }
  public int Quantity { get; set; }
  public decimal PriceAtSnapshot { get; set; }
  public Guid CartId { get; set; }
}
