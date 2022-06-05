using Microsoft.Extensions.DependencyInjection;
using MPR.Shared.Logic;

namespace MPR.Users.Logic
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUsersLogic(this IServiceCollection services)
        {
            services.AddSharedLogic("MPR.Users.Logic");
            return services;
        }
    }
}
