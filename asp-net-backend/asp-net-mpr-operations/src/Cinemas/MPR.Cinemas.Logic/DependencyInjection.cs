using Microsoft.Extensions.DependencyInjection;
using MPR.Shared.Logic;

namespace MPR.Cinemas.Logic
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCinemasLogic(this IServiceCollection services)
        {
            services.AddSharedLogic("MPR.Cinemas.Logic");
            return services;
        }
    }
}
