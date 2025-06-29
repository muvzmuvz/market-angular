using Products.Api.Models;

namespace Products.Api.ModelsDto;

public class UpdateProductDto
{
  public string Description { get; set; }
  public decimal Price { get; set; }
  public string Title { get; set; }
  public string Name { get; set; }
  public int CountProduct { get; set; }
  public string Characteristic { get; set; }
}
