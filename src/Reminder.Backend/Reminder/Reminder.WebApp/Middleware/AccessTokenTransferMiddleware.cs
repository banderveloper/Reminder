using Reminder.Application.Configurations;

namespace Reminder.WebApp.Middleware;

// Transfering access token from cookies to Authorization header
public class AccessTokenTransferMiddleware
{
    private readonly RequestDelegate _next;
    private readonly CookiesConfiguration _cookiesConfiguration;

    public AccessTokenTransferMiddleware(RequestDelegate next, CookiesConfiguration cookiesConfiguration)
    {
        _next = next;
        _cookiesConfiguration = cookiesConfiguration;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        var accessToken = context.Request.Cookies[_cookiesConfiguration.AccessTokenCookieName];

        if (!string.IsNullOrEmpty(accessToken))
        {
            if (context.Request.Headers.ContainsKey("Authorization"))
                context.Request.Headers.Remove("Authorization");

            context.Request.Headers.Add("Authorization", $"Bearer {accessToken}");
        }

        await _next(context);
    }
}

public static class AccessTokenTransferMiddlewareExtensions
{
    public static IApplicationBuilder UseAccessTokenTransfer(this IApplicationBuilder builder) =>
        builder.UseMiddleware<AccessTokenTransferMiddleware>();
}