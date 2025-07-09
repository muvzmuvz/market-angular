using Products.Api.Models;

namespace Products.Api.ModelsDto
{
  public class ProductCreateDto
  {
    public Guid ShopId { get; set; }
    public Guid UserId { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; } = decimal.Zero;
    public string Title { get; set; }
    public Category Category { get; set; }
    public string Name { get; set; }
    public int CountProduct { get; set; }
    public string Characteristic { get; set; }

    public List<string> ImagesBase64  { get; set; }
  }
}
