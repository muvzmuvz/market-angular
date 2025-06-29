namespace Products.Api.Models;

public class ProductFavourity : BaseEntity
{
  public Guid ProductId { get; set; }
  public Product Product { get; set; }
}
