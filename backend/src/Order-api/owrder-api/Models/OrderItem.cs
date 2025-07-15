namespace owrder_api.Models;

public class OrderItem : BaseEntity
{
  public Guid UserId { get; set; }
  public Guid OrderId { get; set; }
  public decimal Price { get; set; }
  public int Quantity { get; set; }
  public Guid ProductId { get; set; }

  public decimal TotalPrice => Price * Quantity;
}
