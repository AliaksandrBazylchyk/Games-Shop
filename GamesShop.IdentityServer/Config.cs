using Duende.IdentityServer.Models;
using GamesShop.Common;
using IdentityModel;

namespace GamesShop.IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope(name: "APIs", displayName: "My all APIs",
                new List<string> {JwtClaimTypes.Name, JwtClaimTypes.Role})
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "FullClient",
                ClientName = "My Custom Client",
                AccessTokenLifetime = 60 * 60 * 24,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                RequireClientSecret = false,
                AllowedScopes = {"APIs"}
            }
        };
}