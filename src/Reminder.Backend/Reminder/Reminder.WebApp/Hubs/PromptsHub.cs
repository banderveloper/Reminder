using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Reminder.Application.Interfaces.Providers;

namespace Reminder.WebApp.Hubs;

[Authorize]
public class PromptsHub : Hub
{
    private readonly IJwtProvider _jwtProvider;
    private long UserId => long.Parse(Context.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    public PromptsHub(IJwtProvider jwtProvider)
    {
        _jwtProvider = jwtProvider;
        Console.WriteLine("Add hub");
    }
    
    public override Task OnConnectedAsync()
    {
        Console.WriteLine("Connected user with " + UserId);
        return Task.CompletedTask;
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine("Disconnected user with " + UserId);
        return Task.CompletedTask;
    }
}