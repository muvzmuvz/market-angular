namespace Products.Api.Models;

public class Image : BaseEntity
{
  public Guid ProductId { get; set; }
  public string ImagePath { get; set; }
}
