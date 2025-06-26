namespace Products.Api.Models;

public class ProductHistory : BaseEntity
{
  public Guid ProductId { get; set; }
  public Product Product { get; set; }
}
