namespace Products.Api.Models;

public class Product : BaseEntity
{
  public Guid ShopId { get; set; }
  public string Description { get; set; }
  public decimal Price { get; set; } = decimal.Zero;
  public string Title { get; set; }
  public Category Category { get; set; }
  public string Name { get; set; }
  public int CountProduct { get; set; }
  public int CountViewProduct { get; set; }
  public string Characteristic { get; set; }

  public List<Image> Images { get; set; } = new List<Image>();
  public List<Review> Reviews { get; set; } = new List<Review>();
}
