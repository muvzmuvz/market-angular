using Microsoft.AspNetCore.Identity;

namespace marketplace_api.Models;

public class UserIdentity : IdentityUser<Guid>
{
  public string FirstName { get; set; }
}
