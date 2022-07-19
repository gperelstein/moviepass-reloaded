using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MPR.Auth.Data.IdentityCustomDbContext;

public class ApiAuthorizationDbContext<TUser> : KeyApiAuthorizationDbContext<TUser, IdentityRole, string>
where TUser : IdentityUser
{
    public ApiAuthorizationDbContext(
        DbContextOptions<MprAuthDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions)
        : base(options, operationalStoreOptions)
    {
    }
}
