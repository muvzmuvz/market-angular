namespace marketplace_api.Models;

public class DomainUser : BaseEntity
{
    public Guid IdentityId { get; init; }
    public string imagePath {  get; set; }
    public Role Role { get; set; }
    public decimal ExpenseSummary { get; set; }
}
