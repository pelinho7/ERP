using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                   new Client
                   {
                        ClientId = "movieClient",
                        AllowedGrantTypes = GrantTypes.ClientCredentials,
                        ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        },
                        AllowedScopes = { "movieAPI" }
                   },
                   new Client
                   {
                       ClientId = "angular_client",
                       ClientName = "Movies MVC Web App",
                       AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                       RequirePkce = false,
                       AllowRememberConsent = false,
                       AccessTokenLifetime=100,
                       AllowOfflineAccess = true,
                       RedirectUris = new List<string>()
                       {
                           //"https://localhost:4200/account/login-redirect",
                           "https://localhost:4200",
                           "https://localhost:4200/assets/silent-renew.html",
                           //"https://localhost:8020",
                           //"https://localhost:8020/assets/silent-renew.html"
                       },
                       PostLogoutRedirectUris = new List<string>()
                       {
                           "https://localhost:4200",
                           //"https://localhost:8020"
                       },
                       AllowedCorsOrigins = { "https://localhost:4200","https://localhost:8020" },
                       //ClientSecrets = new List<Secret>
                       //{
                       //    new Secret("secret".Sha256())
                       //},
                       RequireClientSecret=false,
                       AllowAccessTokensViaBrowser = true,
                       AllowedScopes = new List<string>
                       {
                           IdentityServerConstants.StandardScopes.OpenId,
                           IdentityServerConstants.StandardScopes.Profile,
                           IdentityServerConstants.StandardScopes.Address,
                           IdentityServerConstants.StandardScopes.Email,
                           IdentityServerConstants.StandardScopes.OfflineAccess,
                           "erpAPI",
                           "roles",
                           "companyId",
                           "companyType",
                           "id"
                       }
                   }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
           new ApiScope[]
           {
               new ApiScope("erpAPI", "ERP API")
           };

        public static IEnumerable<ApiResource> ApiResources =>
          new ApiResource[]
          {
               //new ApiResource("movieAPI", "API"){
               //     UserClaims = { "email" }
               // }
          };

        public static IEnumerable<IdentityResource> IdentityResources =>
          new IdentityResource[]
          {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile(),
              new IdentityResources.Address(),
              new IdentityResources.Email(),
              new IdentityResource(
                    "roles",
                    "Your role(s)",
                    new List<string>() { "role" }),
              new IdentityResource
                {
                    Name = "companyId",
                    DisplayName = "companyId",
                    UserClaims = { "companyId" }
                },
              new IdentityResource
                {
                    Name = "companyType",
                    DisplayName = "companyType",
                    UserClaims = { "companyType" }
                },
              new IdentityResource
                {
                    Name = "id",
                    DisplayName = "id",
                    UserClaims = { "id" }
                },
          };
    }
}
