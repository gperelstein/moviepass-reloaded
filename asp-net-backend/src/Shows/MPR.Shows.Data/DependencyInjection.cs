using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MPR.Shows.Logic.Abstractions;

namespace MPR.Shows.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddShowsData(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<IMprShowsDbContext, MprShowsDbContext>();
            var connectionString = configuration.GetConnectionString("MprShows");
            services.AddDbContext<MprShowsDbContext>((options) =>
            {
                options.UseNpgsql(connectionString);
            });

            return services;
        }
    }
}
