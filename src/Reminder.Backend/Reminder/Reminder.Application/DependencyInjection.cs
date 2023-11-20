using Microsoft.Extensions.DependencyInjection;
using Reminder.Application.Interfaces.Services;
using Reminder.Application.Services;

namespace Reminder.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // todo
        services.AddScoped<IUserService, UserService>();
        
        return services;
    }
}