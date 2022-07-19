using Microsoft.Extensions.DependencyInjection;
using MPR.Shared.Logic;

namespace MPR.Movies.Logic
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMoviesLogic(this IServiceCollection services)
        {
            services.AddSharedLogic("MPR.Movies.Logic");
            return services;
        }
    }
}
