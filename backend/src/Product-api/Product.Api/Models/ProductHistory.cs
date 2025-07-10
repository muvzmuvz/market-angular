namespace Products.Api.Models;

public class ProductHistory : BaseEntity
{
  public Guid ProductId { get; set; }
  public string ProductName { get; set; }
  public Guid UserId { get; set; }
}
