using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MPR.Shared.Logic.Abstractions;
using MPR.Shared.Logic.Implementations;
using System.Reflection;

namespace MPR.Shared.Logic
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSharedLogic(this IServiceCollection services, string mediatrAssemblyName = null)
        {
            var targetAssembly = mediatrAssemblyName != null ? AppDomain.CurrentDomain.Load(mediatrAssemblyName) : Assembly.GetExecutingAssembly();

            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.AddValidatorsFromAssembly(targetAssembly);
            services.AddMediatR(targetAssembly);
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IDateTimeService, DateTimeService>();

            return services;
        }
    }
}
