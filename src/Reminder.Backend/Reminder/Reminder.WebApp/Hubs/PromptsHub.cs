using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Reminder.Application;
using Reminder.Application.Interfaces.Services;
using Reminder.WebApp.Models.DisposablePrompts;

namespace Reminder.WebApp.Hubs;

[Authorize]
public class PromptsHub : Hub
{
    private readonly IDisposablePromptService _disposablePromptService;
    private long UserId => long.Parse(Context.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    
    public PromptsHub(IDisposablePromptService disposablePromptService)
    {
        _disposablePromptService = disposablePromptService;
    }
    
    public override async Task OnConnectedAsync()
    {
        Console.WriteLine("Connected " + Context.ConnectionId);
        
        await Groups.AddToGroupAsync(Context.ConnectionId, GetGroupName(UserId));

        var disposablePromptsResult = await _disposablePromptService.GetAllByUserId(UserId);

        await Clients.Caller.SendAsync("GetAllDisposablePrompts", disposablePromptsResult);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine("Disconnected " + Context.ConnectionId);
        
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, GetGroupName(UserId));
    }

    public async Task CreateDisposablePromptAsync(CreateDisposablePromptRequestModel model)
    {
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

    public async Task DeleteDisposablePromptAsync(long disposablePromptId)
    {
        Console.WriteLine("Delete invoked");
        
        var getOwnerIdResult = await _disposablePromptService.GetUserIdByPrompt(disposablePromptId);

        if (!getOwnerIdResult.Succeed)
        {
            await Clients.Group(GetGroupName(UserId)).SendAsync("GetDeleteDisposablePromptError", getOwnerIdResult);
            return;
        }

        var ownerId = getOwnerIdResult.Data;
        Console.WriteLine("Owner id: " + ownerId);
        Console.WriteLine("User id: " + UserId);

        if (ownerId != UserId)
        {
            var errorResult = Result<None>.Error(ErrorCode.DisposablePromptProtected);
            await Clients.Group(GetGroupName(UserId)).SendAsync("GetDeleteDisposablePromptError", errorResult);
            return;
        }

        await _disposablePromptService.DeleteById(disposablePromptId);

        var successResult = Result<long>.Success(disposablePromptId);
        await Clients.Group(GetGroupName(UserId)).SendAsync("GetDeleteDisposablePromptSuccess", successResult);
    }

    public async Task UpdateDisposablePromptAsync(UpdateDisposablePromptRequestModel model)
    {
        Console.WriteLine("Update invoked");
        
        var getOwnerIdResult = await _disposablePromptService.GetUserIdByPrompt(model.Id);

        if (!getOwnerIdResult.Succeed)
        {
            await Clients.Group(GetGroupName(UserId)).SendAsync("GetUpdateDisposablePromptError", getOwnerIdResult);
            return;
        }

        var ownerId = getOwnerIdResult.Data;
       
        if (ownerId != UserId)
        {
            var errorResult = Result<None>.Error(ErrorCode.DisposablePromptProtected);
            await Clients.Group(GetGroupName(UserId)).SendAsync("GetUpdateDisposablePromptError", errorResult);
            return;
        }

        var updatedDisposablePromptResult =
            await _disposablePromptService.Update(model.Id, model.Title, model.Description, model.ShowsAt);

        if (!updatedDisposablePromptResult.Succeed)
        {
            await Clients.Group(GetGroupName(UserId)).SendAsync("GetUpdateDisposablePromptError", updatedDisposablePromptResult);
            return;
        }
        
        await Clients.Group(GetGroupName(UserId)).SendAsync("GetUpdateDisposablePromptSuccess", updatedDisposablePromptResult);
    }
    
    private static string GetGroupName(long userId) => "user-" + userId;
}