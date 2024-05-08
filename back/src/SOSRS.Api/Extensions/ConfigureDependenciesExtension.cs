using SOSRS.Api.Repositories;
using SOSRS.Api.Services;
using SOSRS.Api.Services.Authentication;

namespace SOSRS.Api.Extensions
{
    public static class ConfigureDependenciesExtension
    {
        public static IServiceCollection ConfigureDependencies(
            this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IValidadorService, ValidadorService>();

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<IAbrigoRepository, AbrigoRepository>();
            services.AddScoped<IAnimalRepository, AnimalRepository>();

            return services;
        }
    }
}
