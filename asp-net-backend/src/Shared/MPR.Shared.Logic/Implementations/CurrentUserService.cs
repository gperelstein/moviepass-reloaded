using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using MPR.Shared.Logic.Abstractions;
using System.Security.Claims;

namespace MPR.Shared.Logic.Implementations
{
    internal class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? GetUserId()
        {
            var username = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type.EndsWith(ClaimTypes.NameIdentifier));
            if (Guid.TryParse(username?.Value, out Guid userId))
            {
                return userId;
            }

            return null;
        }

        public string GetToken()
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

            return token;
        }
    }
}
