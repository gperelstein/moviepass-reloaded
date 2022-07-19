using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NSwag.Generation.Processors.Security;

namespace MPR.Movies.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMoviePresentation(this IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy",
                    policy => { policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); });
            });
            services.AddControllers();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = "https://localhost:5000";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = false
                };
            });

            services.AddOpenApiDocument(document =>
            {
                document.Version = "v1";
                document.Title = "Movies";
                document.Description = "Movies Api";
                document.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT"));
                document.AddSecurity("JWT", new NSwag.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Scheme = "Bearer",
                    Type = NSwag.OpenApiSecuritySchemeType.OAuth2,
                    Flow = NSwag.OpenApiOAuth2Flow.Implicit,
                    TokenUrl = "https://localhost:5000/connect/token",
                    AuthorizationUrl = "https://localhost:5000/connect/authorize",
                    In = NSwag.OpenApiSecurityApiKeyLocation.Header
                });
            });

            services.AddMvc();

            return services;
        }
    }
}
