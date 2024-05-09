using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SOSRS.Api.Configuration;
using SOSRS.Api.Extensions;

namespace TestesIntegracao
{
    public static class Injections
    {
        public static IServiceProvider GetProviders()
        {
            var configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

            var services = new ServiceCollection();
            services.AddDatabaseConfiguration(configuration);
            services.ConfigureServices(configuration);
            services.ConfigureAuth(configuration);

            return services.BuildServiceProvider();
        }
    }
}
