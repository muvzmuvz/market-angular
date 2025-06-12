using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using marketplace_api.Common.interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace marketplace_api.IdentityServer;

public class CustomProfileService : IProfileService
{
  private readonly UserManager<IdentityUser<Guid>> _userManager;

  public CustomProfileService(
    UserManager<IdentityUser<Guid>> userManager)
  {
    _userManager = userManager;
  }

  public async Task GetProfileDataAsync(ProfileDataRequestContext context)
  {
    var user = await _userManager.GetUserAsync(context.Subject);
    var roles = await _userManager.GetRolesAsync(user);

    var claims = new List<Claim>()
    {
      new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
    };

    foreach (var role in roles)
    {
      claims.Add(new Claim("role", role));
    }

    context.IssuedClaims.AddRange(claims);
  }

  public async Task IsActiveAsync(IsActiveContext context)
  {
    var user = await _userManager.GetUserAsync(context.Subject);
    context.IsActive = user != null;
  }
}
