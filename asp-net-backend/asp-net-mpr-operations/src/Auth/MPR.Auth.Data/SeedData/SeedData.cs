using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MPR.Auth.Data.IdentityConfiguration;
using MPR.Shared.Domain.Authorization;
using MPR.Shared.Domain.Models;
using System.Security.Claims;

namespace MPR.Auth.Data.SeedData;

public class SeedData
{
    public static void SeedIdentityData(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var configuration = scope.ServiceProvider.GetService<IConfiguration>();
            var context = scope.ServiceProvider.GetService<MprAuthDbContext>();

            scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
            var configContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

            configContext.Database.Migrate();
            context.Database.Migrate();

            // Clients seeding
            var clients = Config.Clients;
            foreach (var client in clients)
            {
                var existingClient = configContext.Clients
                                        .Include(x => x.AllowedGrantTypes)
                                        .Include(x => x.ClientSecrets)
                                        .Include(x => x.AllowedScopes)
                                        .Include(x => x.Claims)
                                        .Include(x => x.RedirectUris)
                                        .Include(x => x.AllowedCorsOrigins)
                                        .Include(x => x.PostLogoutRedirectUris)
                                        .FirstOrDefault(x => x.ClientId == client.ClientId);
                if (existingClient == null)
                {
                    configContext.Clients.Add(client.ToEntity());
                }
                else
                {
                    var clientEntity = client.ToEntity();
                    existingClient.AllowedGrantTypes = clientEntity.AllowedGrantTypes;
                    existingClient.ClientSecrets = clientEntity.ClientSecrets;
                    existingClient.AllowedScopes = clientEntity.AllowedScopes;
                    existingClient.Claims = clientEntity.Claims;
                    existingClient.ClientName = clientEntity.ClientName;
                    existingClient.RequireConsent = clientEntity.RequireConsent;
                    existingClient.AllowAccessTokensViaBrowser = clientEntity.AllowAccessTokensViaBrowser;
                    existingClient.EnableLocalLogin = clientEntity.EnableLocalLogin;
                    existingClient.RedirectUris = clientEntity.RedirectUris;
                    existingClient.AllowedCorsOrigins = clientEntity.AllowedCorsOrigins;
                    existingClient.PostLogoutRedirectUris = clientEntity.PostLogoutRedirectUris;
                    existingClient.AlwaysIncludeUserClaimsInIdToken = clientEntity.AlwaysIncludeUserClaimsInIdToken;
                    existingClient.AccessTokenLifetime = clientEntity.AccessTokenLifetime;
                    existingClient.RequireClientSecret = clientEntity.RequireClientSecret;
                }
            }
            var deletedClients = configContext.Clients.AsEnumerable().Where(x => !clients.Any(c => c.ClientId == x.ClientId));
            foreach (var deletedClient in deletedClients)
            {
                configContext.Clients.Remove(deletedClient);
            }
            configContext.SaveChanges();

            // Identity Resources seeding
            var identityResources = Config.IdentityResources;
            foreach (var identityResource in identityResources)
            {
                if (!configContext.IdentityResources.Any(x => x.Name == identityResource.Name))
                    configContext.IdentityResources.Add(identityResource.ToEntity());
            }
            configContext.SaveChanges();

            // Roles seeding
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
            var rolePermissions = RoleConfig.Roles;
            foreach (var rolePermission in rolePermissions)
            {
                if (!context.Roles.Any(a => a.Name == rolePermission.Name))
                {
                    var result = roleManager.CreateAsync(rolePermission).Result;
                }
            }
            context.SaveChanges();

            foreach (var rolePermission in rolePermissions)
            {
                var ahpRole = context.Roles.FirstOrDefault(x => x.Name == rolePermission.Name);
                var currentClaims = roleManager.GetClaimsAsync(ahpRole).Result;
                foreach (var claim in currentClaims.Where(x => x.Type.StartsWith("permissions.")))
                {
                    var result = roleManager.RemoveClaimAsync(ahpRole, claim).Result;
                }
            }
        }
    }

    public static async Task SeedUsers(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var context = scope.ServiceProvider.GetService<MprAuthDbContext>();

            var users = new[]
            {
                new
                {
                    Email = "admin@moviepass.com",
                    UserName = "admin@moviepass.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    Birthday = new DateTime(1990, 1, 1),
                    Roles = new string[] { RoleCodes.ADMIN },
                    Claims = new Dictionary<string, string>()
                }
            };

            foreach (var user in users)
            {
                var existingUser = userMgr.FindByNameAsync(user.UserName).Result;
                if (existingUser == null)
                {
                    var newUser = new User
                    {
                        Email = user.Email,
                        UserName = user.UserName,
                        EmailConfirmed = true
                    };
                    var result = userMgr.CreateAsync(newUser, "Test*1234").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    result = userMgr.AddToRolesAsync(newUser, user.Roles).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    var claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Email, user.Email),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Subject, newUser.Id.ToString())
                    };
                    if (user.Claims != null)
                    {
                        foreach (var claim in user.Claims)
                        {
                            claims.Add(new Claim(claim.Key, claim.Value));
                        }
                    }
                    result = userMgr.AddClaimsAsync(newUser, claims).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                    var profile = new Profile
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Birthday = user.Birthday,
                        UserId = newUser.Id
                    };
                    await context.Profiles.AddAsync(profile);
                    await context.SaveChangesAsync(newUser.Id);
                    Console.WriteLine($"{ user.UserName } created");
                }
                else
                {
                    Console.WriteLine($"{ user.UserName } already exists");
                }
            }
        }
    }

    public static void Migrate(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var context = scope.ServiceProvider.GetService<MprAuthDbContext>();
            context.Database.Migrate();

            var configurationContext = scope.ServiceProvider.GetService<ConfigurationDbContext>();
            configurationContext.Database.Migrate();

            var persistedGrantContext = scope.ServiceProvider.GetService<PersistedGrantDbContext>();
            persistedGrantContext.Database.Migrate();
        }
    }
}
