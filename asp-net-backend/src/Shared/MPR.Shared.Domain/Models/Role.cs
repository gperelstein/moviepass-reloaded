using Microsoft.AspNetCore.Identity;

namespace MPR.Shared.Domain.Models
{
    public class Role : IdentityRole<Guid>
    {
        public Role(string name) : base(name) { }
    }
}
