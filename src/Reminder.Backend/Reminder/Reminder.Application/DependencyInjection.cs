using Microsoft.Extensions.DependencyInjection;
using Reminder.Application.Interfaces.Services;
using Reminder.Application.Providers;
using Reminder.Application.Services;

namespace Reminder.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRefreshSessionService, RefreshSessionService>();

        services.AddSingleton<JwtProvider>();
        services.AddSingleton<CookieProvider>();
        
        return services;
    }
}