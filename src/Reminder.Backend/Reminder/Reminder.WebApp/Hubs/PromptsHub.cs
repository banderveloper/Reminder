using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Reminder.Application.Interfaces.Providers;

namespace Reminder.WebApp.Hubs;


[Authorize]
public class PromptsHub : Hub
{
    private readonly IJwtProvider _jwtProvider;

    public PromptsHub(IJwtProvider jwtProvider)
    {
        _jwtProvider = jwtProvider;
        Console.WriteLine("Add hub");
    }
    
    public override Task OnConnectedAsync()
    {
        Console.WriteLine("Connected");
        return Task.CompletedTask;
    }
}