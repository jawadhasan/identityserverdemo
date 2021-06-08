using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IDPDemoApp.Web
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> {"role"}
                }
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {

                new ApiResource("api1", "My API")
                {
                    Scopes = { "api1" }
                },

                new ApiResource{
                    Name = "customAPI",
                    DisplayName = "Custom API",
                    Description = "Custom API Access",
                    UserClaims = new List<string> {"role"},
                    ApiSecrets = new List<Secret> {new Secret("scopeSecret".Sha256())},
                    //Scopes = new List<Scope>
                    //{
                    //    new Scope("customAPI.read"),
                    //    new Scope("customAPI.write")
                    //}
                }

            };
    

   
       
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                //List of Scopes, which our auth server is allowed to give to different clients.
                //Having scopes here doesn't mean that client can request the scope.

                new ApiScope("api1", "My API"),
                new ApiScope("companyApi", "Company Api"),
                new ApiScope("api1.read", "Read only access"),
                new ApiScope("api1.emailNotify", "email subscription")
            };
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {

                //m2m client (no user)
                new Client
                {
                    ClientId = "m2mclient",
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
                },


                //ROPACC
                //allows us to trade user credentials for the token. Additionally, we can use this flow to exchange only the ClientId and the Secret for the token
                new Client
                {
                    ClientId = "ropacc",
                    ClientName = "Example Client Credentials Client Application",
                    ClientSecrets = new [] { new Secret("secret".Sha512()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes =
                    {
                       IdentityServerConstants.StandardScopes.OpenId, 
                       IdentityServerConstants.StandardScopes.Profile,
                       "api1",
                       "role",
                       IdentityServerConstants.StandardScopes.Email
                    }
                },
               
                //ROPC client - similar to ROPACC
                new Client
                {
                    ClientId  = "ropcClient",

                    ClientSecrets = new [] { new Secret("secret".Sha512()) },

                    //means, we trade username/password for given-user for access//use is not recommended
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                },

                


                //mvc client (confidential client)
                new Client
                {
                    ClientId = "mvcapp",
                    ClientName = "confidential client",

                    AllowedGrantTypes = GrantTypes.Code, //AuthorizationCode (this is redirection based flow)
                    RequirePkce = true, //recommended to avoid authorization code injection attacks

                    //because its rediction based flow, we need to add a valid URI where this client is allowed to receive token
                    RedirectUris = new List<string>
                    {
                        "http://localhost:44388/signin-oidc" // address of requesting client application
                    },

                    //we also want to specify, which scopes are allowed to be requested by the requesting client 
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },

                    //lastly, we need to configure secret, that's used for client authentication to allow client application to call token endpoint
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    }

                    
                },
                
                //angular client
                new Client
                {
                    ClientId = "angular-client",
                    ClientName = "Angular-Client",
                    AllowedGrantTypes = GrantTypes.Code, //Authorization-Code Flow
                    RedirectUris = new List<string>
                    {
                        "http://localhost:4200/signin-callback", 
                        "http://localhost:4200/assets/silent-callback.html"
                    },
                    RequirePkce = true,
                    AllowAccessTokensViaBrowser = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1",
                        "companyApi"
                    },
                    AllowedCorsOrigins = { "http://localhost:4200" },
                    RequireClientSecret = false,
                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:4200/signout-callback"
                    },
                    RequireConsent = false,
                    AccessTokenLifetime = 600 //10 minutes.
                }
            };
        public static List<TestUser> TestUsers => new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "1", //shall be unique at level of this idp
                Username = "Jon",
                Password = "jon123",

                Claims = new List<Claim>
                {
                    new Claim("given_name","jon"),
                    new Claim("family_name", "doe"),
                    new Claim(JwtClaimTypes.Email, "jon@test.com"),
                    new Claim(JwtClaimTypes.Role, "companyAdmin"),
                }
            },
            new TestUser
            {
                SubjectId = "2",
                Username = "Jane",
                Password = "jane123",

                Claims = new List<Claim>
                {
                    new Claim("given_name","jane"),
                    new Claim("family_name", "doe"),
                    new Claim(JwtClaimTypes.Email, "jane@test.com"),
                    new Claim(JwtClaimTypes.Role, "writer"),
                }
            }
        };
    }
}
