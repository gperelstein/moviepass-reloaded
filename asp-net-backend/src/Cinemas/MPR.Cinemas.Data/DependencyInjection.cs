using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MPR.Cinemas.Logic.Abstractions;

namespace MPR.Cinemas.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCinemasData(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<IMprCinemasDbContext, MprCinemasDbContext>();
            var connectionString = configuration.GetConnectionString("MprCinemas");
            services.AddDbContext<MprCinemasDbContext>((options) =>
            {
                options.UseNpgsql(connectionString);
            });

            return services;
        }
    }
}
