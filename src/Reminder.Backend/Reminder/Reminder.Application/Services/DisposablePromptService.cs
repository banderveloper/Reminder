using Reminder.Application.Interfaces.Services;
using Reminder.Domain.Entities.Database;

namespace Reminder.Application.Services;

public class DisposablePromptService : IDisposablePromptService
{
    public Task<Result<long>> GetUserIdByPrompt(long disposablePromptId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IQueryable<DisposablePrompt>>> GetAllByUserId(long userId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<DisposablePrompt>> Create(long userId, string title, string? description, DateTime showsAt)
    {
        throw new NotImplementedException();
    }

    public Task<Result<None>> DeleteById(long disposablePromptId)
    {
        throw new NotImplementedException();
    }
}