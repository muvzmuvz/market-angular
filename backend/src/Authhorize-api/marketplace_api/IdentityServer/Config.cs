using Duende.IdentityModel;
using Duende.IdentityServer.Models;
using System.Net.NetworkInformation;

namespace marketplace_api.IdentityServer;

public static class Config
{
  public static IEnumerable<ApiResource> ApiResources =>
    new List<ApiResource>
    {
        new ApiResource("api", "internal API", new[]
        {JwtClaimTypes.Name})
        {
            Scopes = { "api" }
        }
    };


  public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("roles", new[] { "role" })
        };

  public static IEnumerable<ApiScope> ApiScopes =>
      new ApiScope[]
      {
            new ApiScope("web", "web API"),
            new ApiScope("api", "internal API"),
      };

  public static IEnumerable<Client> Clients =>
      new Client[]
      {
            new Client
            {
                ClientId = "client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("wildbobr".Sha256()) },
                AllowedScopes = { "web" }
            },
            new Client
            {
                ClientId = "web",
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "http://localhost:4200" },
                PostLogoutRedirectUris = { "http://localhost:4200" },
                AllowedCorsOrigins = { "http://localhost:4200"},
                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "api", "roles", "offline_access" },
                RequireClientSecret = false,
                RequirePkce = true
            },
            

      };
}
