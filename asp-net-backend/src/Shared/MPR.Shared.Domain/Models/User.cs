using Microsoft.AspNetCore.Identity;

namespace MPR.Shared.Domain.Models
{
    public class User : IdentityUser<Guid>
    {
        public bool IsActive { get; set; }
    }
}
