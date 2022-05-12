using Duende.IdentityServer.Models;
using GamesShop.Common;

namespace GamesShop.IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope(name: Secrets.CatalogApiScopeName, displayName: Secrets.CatalogApiScopeDisplayName),
            new ApiScope(name: Secrets.CheckoutApiScopeName, displayName: Secrets.CheckoutApiScopeDisplayName)
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = Secrets.CommonerId,
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret(Secrets.CommonerSecretKey.Sha256())},
                AllowedScopes = {Secrets.CatalogApiScopeName}
            }
        };
}