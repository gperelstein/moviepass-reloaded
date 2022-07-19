using Microsoft.Extensions.DependencyInjection;
using MPR.Movies.Logic.Abstractions;

namespace MPR.Movies.TheMovieDb
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTheMovieDb(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddTransient<ITheMovieDbClient, TheMovieDbClient>();

            return services;
        }
    }
}
