using Products.Api.Models;
using Products.Api.ModelsDto;

namespace Products_Api.ModelsDto;

public class ProductFavourityDto
{
  public Guid UserId { get; set; }
  public Guid ProductId { get; set; }
  public ProductDto Product { get; set; }
}
