using Microsoft.AspNetCore.Identity;

namespace marketplace_api.Models;

public class UserIdentity : IdentityUser<Guid>
{
  public string FirstName { get; set; }
  public string imagePath { get; set; } = "defoultImage";
  public decimal ExpenseSummary { get; set; }
}
