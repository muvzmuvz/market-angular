namespace Products.Api.Models;

public class Review : BaseEntity
{
  public string Description { get; set; }
  public Guid Userid { get; set; }
  public Guid ProductId { get; set; }
  public sbyte Rating { get; set; }
}
