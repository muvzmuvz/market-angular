namespace marketplace_api.Models;

public class ShopSeller : BaseEntity
{
  public Guid ShopId { get; set; }
  public Shop Shop { get; set; }

  public Guid SellerId { get; set; }
  public DomainUser Seller { get; set; }
}
