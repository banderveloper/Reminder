using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reminder.Application.Configurations;
using Reminder.Application.Interfaces;

namespace Reminder.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, string environmentName)
    {
        var scope = services.BuildServiceProvider().CreateScope();
        var databaseConfiguration = scope.ServiceProvider.GetRequiredService<DatabaseConfiguration>();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var filledConnectionString = string.Format(databaseConfiguration.ConnectionStringPattern,
                databaseConfiguration.Username, databaseConfiguration.Password);

            switch (environmentName.ToLower())
            {
                case "development":
                    options.UseSqlite(filledConnectionString);
                    break;
                case "dockerdevelopment":
                case "preproduction":
                case "production":
                    options.UseNpgsql(filledConnectionString);
                    break;
                default:
                    // temp
                    throw new Exception("Unknown environment name");
            }

            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
        
        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}