namespace owrder_api.Models;

public class Order : BaseEntity
{
  public Guid UserId { get; set; }
  public string CustomerName { get; set; } = string.Empty;
  public string CustomerEmail { get; set; } = string.Empty;
  public string ShippingAddress { get; set; } = string.Empty;
  public decimal TotalAmount { get; set; }

  public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
