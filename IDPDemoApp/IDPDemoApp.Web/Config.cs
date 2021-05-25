using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;

namespace IDPDemoApp.Web
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("api1", "My API"),
                new ApiScope("api1.read", "Read only access"),
                new ApiScope("api1.emailNotify", "email subscription"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    ClientName = "m2mclient",

                    // use the client-id/secret for authentication. Use this grant-type when no user is involved
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                }
            };
    }
}
