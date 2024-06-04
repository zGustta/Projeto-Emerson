using Duende.IdentityServer.Models;
using Duende.IdentityServer;

namespace FatecSisMed.IdentityServer.Configuration
{
    public class IdentityConfiguration
    {
        public const string Admin = "Admin";
        public const string Client = "Client";

        public static IEnumerable<IdentityResource>
            IdentityResources => new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()

            };
        public static IEnumerable<ApiScope>
            ApiScopes => new List<ApiScope>
            {
                new ApiScope("fatecsismed", "FatecSisMed Server"),
                new ApiScope(name: "read", "Read data."),
                new ApiScope(name:"write", "Write data."),
                new ApiScope(name:"delete", "Delete data.")
            };
        public static IEnumerable<Client>
            Clients => new List<Client>
            {
             new Client
             {
                 ClientId="client",
                 ClientSecrets = { new Secret("vai_reprovar".Sha256()) },
                 AllowedGrantTypes = GrantTypes.ClientCredentials,
                 AllowedScopes = {"read", "write", "profiles"}
            },
             new Client
             {
                 ClientId = "fatecsismed",
                 ClientSecrets = { new Secret("vai_reprovar".Sha256()) },
                 RedirectUris = {"https://localhost:7256/signin-oidc"},
                 PostLogoutRedirectUris = {"https://localhost:7256/signout-callback-oidc"},
                 AllowedScopes = new List<string>
                 {
                     IdentityServerConstants.StandardScopes.OpenId,
                     IdentityServerConstants.StandardScopes.Profile,
                     IdentityServerConstants.StandardScopes.Email,
                     "fatecsismed"
                 }
             }


            };
    }
}
