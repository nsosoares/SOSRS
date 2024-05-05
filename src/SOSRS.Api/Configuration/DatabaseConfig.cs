using Microsoft.EntityFrameworkCore;
using SOSRS.Api.Data;

namespace SOSRS.Api.Configuration;

public static class DatabaseConfig
{
    public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }

    public static void SincroniseDatabaseEF(this IServiceCollection services)
    {
        using var serviceProvider = services.BuildServiceProvider();
        var context = serviceProvider.GetRequiredService<AppDbContext>();

        if (context.Database.GetPendingMigrations().Any())
            context.Database.Migrate();
    }
}
