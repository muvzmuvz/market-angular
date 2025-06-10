using Microsoft.AspNetCore.Identity;

namespace marketplace_api.IdentityServer;

public class CreateRoles
{
  public static async Task CreateRole(IServiceProvider serviceProvider)
  {
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    string[] roleNames = { "Admin", "Seller", "User" };

    foreach (var roleName in roleNames)
    {
      var roleExist = await roleManager.RoleExistsAsync(roleName);
      if (!roleExist)
      {
        await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
      }
    }
  }
}
