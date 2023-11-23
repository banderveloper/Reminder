﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Reminder.Application;
using Reminder.Application.Interfaces.Providers;
using Reminder.Application.Interfaces.Services;
using Reminder.Domain.Entities.Database;
using Reminder.WebApp.Models.DisposablePrompts;

namespace Reminder.WebApp.Hubs;

[Authorize]
public class PromptsHub : Hub
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IDisposablePromptService _disposablePromptService;
    private long UserId => long.Parse(Context.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    // private static Dictionary<long, ISet<string>> _userConnections = new(); 

    public PromptsHub(IJwtProvider jwtProvider, IDisposablePromptService disposablePromptService)
    {
        _jwtProvider = jwtProvider;
        _disposablePromptService = disposablePromptService;
    }
    
    public override async Task OnConnectedAsync()
    {
        Console.WriteLine("Connected user with " + UserId);

        // _userConnections.AddOrUpdate(UserId, Context.ConnectionId);
        await Groups.AddToGroupAsync(Context.ConnectionId, GetGroupName(UserId));

        var disposablePromptsResult = await _disposablePromptService.GetAllByUserId(UserId);

        await Clients.Caller.SendAsync("GetAllDisposablePrompts", disposablePromptsResult);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine("Disconnected user with " + UserId);

        // _userConnections[UserId].Remove(Context.ConnectionId);
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, GetGroupName(UserId));
    }

    public async Task CreateDisposablePromptAsync(CreateDisposablePromptRequestModel model)
    {
        Console.WriteLine("Creation invoked from user with id " + UserId);
        
        var createDisposablePromptResult =
            await _disposablePromptService.Create(UserId, model.Title, model.Description, model.ShowsAt);

        if (!createDisposablePromptResult.Succeed)
        {
            await Clients.Caller.SendAsync("GetCreateDisposablePromptError", createDisposablePromptResult);
            return;
        }

        await Clients.Group(GetGroupName(UserId))
            .SendAsync("GetCreateDisposablePromptSuccess", createDisposablePromptResult);
    }

    private static string GetGroupName(long userId) => "user-" + userId;
}