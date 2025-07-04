using Products.Api.Models;

namespace Products.Api.ModelsDto;

public class ProductDto
{
  public Guid Id { get; set; }
  public Guid ShopId { get; set; }
  public string Description { get; set; }
  public decimal Price { get; set; } = decimal.Zero;
  public string Title { get; set; }
  public Category Category { get; set; }
  public string Name { get; set; }
  public int CountProduct { get; set; }
  public int CountViewProduct { get; set; }
  public ProductStatus productStatus { get; set; }
  public string Characteristic { get; set; }
  public DateTime DateCreated { get; set; }

  public List<Image> Images { get; set; } = new List<Image>();
}
