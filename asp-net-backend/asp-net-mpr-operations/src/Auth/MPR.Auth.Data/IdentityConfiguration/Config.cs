﻿using Duende.IdentityServer.Models;

namespace MPR.Auth.Data.IdentityConfiguration
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
            new ApiScope("api")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
            // m2m client credentials flow client
            new Client
            {
                ClientId = "m2m.client",
                ClientName = "Client Credentials Client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                AllowedScopes = { "api" }
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "interactive",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "http://localhost:3000/callback" },
                FrontChannelLogoutUri = "http://localhost:3000/signout",
                PostLogoutRedirectUris = { "http://localhost:3000" },
                AllowedCorsOrigins =     { "http://localhost:3000" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "api" }
            },
            };
    }
}
