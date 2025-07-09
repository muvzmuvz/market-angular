using Products.Api.Models;

namespace Products_Api.ModelsDto;

public class CartItemDto
{
  public Guid ProductId { get; set; }
  public int Quantity { get; set; }
}
