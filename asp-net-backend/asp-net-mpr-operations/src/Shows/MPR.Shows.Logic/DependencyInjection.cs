using Microsoft.Extensions.DependencyInjection;
using MPR.Shared.Logic;

namespace MPR.Shows.Logic
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddShowsLogic(this IServiceCollection services)
        {
            services.AddSharedLogic("MPR.Shows.Logic");
            return services;
        }
    }
}
