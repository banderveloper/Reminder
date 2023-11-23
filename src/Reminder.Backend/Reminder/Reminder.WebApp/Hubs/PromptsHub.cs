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

    // private static Dictionary<long, ISet<string>> _userConnections = new(); 

    public PromptsHub(IJwtProvider jwtProvider)
    {
        _jwtProvider = jwtProvider;
    }
    
    public override async Task OnConnectedAsync()
    {
        Console.WriteLine("Connected user with " + UserId);

        // _userConnections.AddOrUpdate(UserId, Context.ConnectionId);
        await Groups.AddToGroupAsync(Context.ConnectionId, GetGroupName(UserId));
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine("Disconnected user with " + UserId);

        // _userConnections[UserId].Remove(Context.ConnectionId);
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, GetGroupName(UserId));
    }

    private string GetGroupName(long userId) => "user-" + userId;
}