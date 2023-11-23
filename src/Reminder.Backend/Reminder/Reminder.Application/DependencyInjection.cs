using Microsoft.Extensions.DependencyInjection;
using Reminder.Application.Interfaces.Providers;
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
        services.AddScoped<IDisposablePromptService, DisposablePromptService>();

        services.AddSingleton<IJwtProvider, JwtProvider>();
        services.AddSingleton<ICookieProvider, CookieProvider>();
        services.AddSingleton<IEncryptionProvider, Sha256Provider>();
        
        return services;
    }
}