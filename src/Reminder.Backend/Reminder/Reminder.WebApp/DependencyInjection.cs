using Microsoft.Extensions.Options;
using Reminder.Application.Configurations;

namespace Reminder.WebApp;

public static class DependencyInjection
{
    // Inject custom configuration classes, dependent from appsettings
    public static void AddCustomConfiguration(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<DatabaseConfiguration>(builder.Configuration.GetSection(DatabaseConfiguration.ConfigurationKey));
        builder.Services.AddSingleton(resolver =>
            resolver.GetRequiredService<IOptions<DatabaseConfiguration>>().Value);

        builder.Services.Configure<CookiesConfiguration>(builder.Configuration.GetSection(CookiesConfiguration.ConfigurationKey));
        builder.Services.AddSingleton(resolver =>
            resolver.GetRequiredService<IOptions<CookiesConfiguration>>().Value);

        builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection(JwtConfiguration.ConfigurationKey));
        builder.Services.AddSingleton(resolver =>
            resolver.GetRequiredService<IOptions<JwtConfiguration>>().Value);

        builder.Services.Configure<RedisConfiguration>(builder.Configuration.GetSection(RedisConfiguration.ConfigurationKey));
        builder.Services.AddSingleton(resolver =>
            resolver.GetRequiredService<IOptions<RedisConfiguration>>().Value);

        builder.Services.Configure<RefreshSessionConfiguration>(builder.Configuration.GetSection(RefreshSessionConfiguration.ConfigurationKey));
        builder.Services.AddSingleton(resolver =>
            resolver.GetRequiredService<IOptions<RefreshSessionConfiguration>>().Value);
    }
}