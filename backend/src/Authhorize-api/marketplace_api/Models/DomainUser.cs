namespace marketplace_api.Models;

public class DomainUser : BaseEntity
{
    public Guid IdentityId { get; set; }
  public string imagePath { get; set; } = "defoultImage";
    public Role Role { get; set; }
    public decimal ExpenseSummary { get; set; }
}
