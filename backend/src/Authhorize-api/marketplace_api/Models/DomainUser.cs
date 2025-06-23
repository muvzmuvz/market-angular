namespace marketplace_api.Models;

public class DomainUser : BaseEntity
{
    public Guid IdentityId { get; set; }
    public string imagePath { get; set; } = "defoultImage";
    public decimal ExpenseSummary { get; set; }
}
