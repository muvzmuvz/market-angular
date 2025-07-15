namespace marketplace_api.Models;

public class Invitation : BaseEntity
{
  public string Email { get; set; } 
  public int ShopId { get; set; }
  public Shop Shop { get; set; }
  public bool IsAccepted { get; set; }
}
