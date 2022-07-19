using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MPR.Auth.Data.Services;
using MPR.Shared.Domain.Models;

namespace MPR.Auth.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthData(this IServiceCollection services,
        IConfiguration configuration)
    {
        var migrationsAssembly = typeof(MprAuthDbContext).Assembly.GetName().Name;
        var connectionString = configuration.GetConnectionString("MprAuth");
        services.AddDbContext<MprAuthDbContext>((options) =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddIdentity<User, Role>(config =>
        {
            config.SignIn.RequireConfirmedEmail = true;
            config.Password.RequireDigit = true;
            config.Password.RequireLowercase = true;
            config.Password.RequireNonAlphanumeric = true;
            config.Password.RequireUppercase = true;
            config.Password.RequiredLength = 8;
            config.Password.RequiredUniqueChars = 0;
        })
        .AddEntityFrameworkStores<MprAuthDbContext>()
        .AddDefaultTokenProviders();

        services.AddIdentityServer()
        .AddDeveloperSigningCredential()        //This is for dev only scenarios when you don’t have a certificate to use.
        .AddConfigurationStore(options =>
        {
            options.ConfigureDbContext = b => b.UseNpgsql(connectionString,
                sql => sql.MigrationsAssembly(migrationsAssembly));
            options.DefaultSchema = "auth";
        })
        .AddOperationalStore(options =>
        {
            options.ConfigureDbContext = b => b.UseNpgsql(connectionString,
                sql => sql.MigrationsAssembly(migrationsAssembly));
            options.DefaultSchema = "auth";
        })
        .AddProfileService<CustomClaimsService<User>>();

        return services;
    }
}
