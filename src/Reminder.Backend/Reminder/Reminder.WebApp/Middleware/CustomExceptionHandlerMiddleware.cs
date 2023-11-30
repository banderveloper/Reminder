using System.Net;
using Reminder.Application;
using SQLitePCL;
using StackExchange.Redis;

namespace Reminder.WebApp.Middleware;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

    public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception.ToString());

        context.Response.ContentType = "application/json";

        switch (exception)
        {
            case RedisConnectionException:
                context.Response.StatusCode = 503;
                await context.Response.WriteAsJsonAsync(Result<None>.Error(ErrorCode.AuthenticationServiceUnavailable));
                break;
            
            default:
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(Result<None>.Error(ErrorCode.Unknown));
                break;
        }
    }
}

public static class CustomExceptionHandlerMiddlewareExtension
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
    }
}