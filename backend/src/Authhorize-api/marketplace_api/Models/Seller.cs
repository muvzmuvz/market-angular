namespace marketplace_api.Models;

public class Seller : BaseEntity
{
  public decimal Profit { get; set; }
  public Guid UserId  { get; set; }
}
