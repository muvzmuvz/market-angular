namespace owrder_api.Models;

public class ReviewAnswer : BaseEntity
{
  public Guid ShopOwner { get; set; }
  public Guid ReviewId { get; set; }
  public string Answer { get; set; } = string.Empty;
}
