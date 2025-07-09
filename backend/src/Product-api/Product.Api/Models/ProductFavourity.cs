namespace Products.Api.Models;

public class ProductFavourity : BaseEntity
{
  public Guid UserId { get; set; }
  public Guid ProductId { get; set; }
  public Product Product { get; set; }
}
