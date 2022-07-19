using Microsoft.Extensions.DependencyInjection;
using MPR.Shared.Logic;

namespace MPR.Tickets.Logic
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTicketsLogic(this IServiceCollection services)
        {
            services.AddSharedLogic("MPR.Tickets.Logic");
            return services;
        }
    }
}
