using Microsoft.Extensions.DependencyInjection;
using MPR.Shared.Logic;

namespace MPR.Payments.Logic
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPaymentsLogic(this IServiceCollection services)
        {
            services.AddSharedLogic("MPR.Payments.Logic");
            return services;
        }
    }
}
