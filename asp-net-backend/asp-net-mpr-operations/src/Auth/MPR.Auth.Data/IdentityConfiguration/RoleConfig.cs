using MPR.Shared.Domain.Authorization;
using MPR.Shared.Domain.Models;

namespace MPR.Auth.Data.IdentityConfiguration;

public static class RoleConfig
{
    public static IEnumerable<Role> Roles =>
        new List<Role>
        {
            new Role(RoleCodes.ADMIN),
            new Role(RoleCodes.REGULAR_USER)
        };
}
