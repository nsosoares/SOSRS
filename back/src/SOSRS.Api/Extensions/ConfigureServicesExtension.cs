using SOSRS.Api.Configuration;

namespace SOSRS.Api.Extensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtConfiguration =
                configuration
                    .GetSection("JWT")
                    .Get<JWTConfiguration>();

            if(jwtConfiguration != null)
            {
                services.AddSingleton(jwtConfiguration);
            }

            return services;
        }
    }
}
