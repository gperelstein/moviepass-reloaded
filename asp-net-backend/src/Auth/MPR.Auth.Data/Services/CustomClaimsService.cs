using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MPR.Shared.Domain.Models;
using System.Security.Claims;

namespace MPR.Auth.Data.Services;

public class CustomClaimsService<T> : IProfileService
{
    protected UserManager<User> _userManager;
    private readonly IUserClaimsPrincipalFactory<User> _claimsFactory;

    public CustomClaimsService(UserManager<User> userManager,
        IUserClaimsPrincipalFactory<User> claimsFactory)
    {
        _userManager = userManager;
        _claimsFactory = claimsFactory;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var sub = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(sub);

        if (user == null)
        {
            return;
        }
        var principal = await _claimsFactory.CreateAsync(user);

        var issuedClaims = new List<Claim>
        {
            new Claim("userId", user.Id.ToString()),
            GetRolesClaim(context, principal),
        };

        if (issuedClaims.Any())
        {
            context.IssuedClaims.AddRange(issuedClaims);
        }
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var userId = context.Subject.GetSubjectId();
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == new Guid(userId));

        if (user == null)
        {
            context.IsActive = false;
            return;
        }

        context.IsActive = true;
    }

    private static Claim GetRolesClaim(ProfileDataRequestContext context, ClaimsPrincipal principal)
    {
        var roleClaims = string.Join(",", principal.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList());
        return new Claim(ClaimTypes.Role, roleClaims);
    }
}
