using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MPR.Movies.Logic.Abstractions;

namespace MPR.Movies.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMoviesData(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<IMprMoviesDbContext, MprMoviesDbContext>();
            var connectionString = configuration.GetConnectionString("MprMovies");
            services.AddDbContext<MprMoviesDbContext>((options) =>
            {
                options.UseNpgsql(connectionString);
            });

            return services;
        }
    }
}
