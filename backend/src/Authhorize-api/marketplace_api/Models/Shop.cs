namespace marketplace_api.Models;

public class Shop : BaseEntity
{
  public string Name { get; set; }
  public string Description { get; set; }
  public Guid OwnerId { get; set; }
  public DomainUser Owner { get; set; }
  public decimal Profit { get; set; }
  public Guid UserId  { get; set; }
  public ICollection<ShopSeller> Sellers { get; set; }
}
