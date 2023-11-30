using Microsoft.EntityFrameworkCore;
using Reminder.Application.Interfaces;
using Reminder.Application.Interfaces.Services;
using Reminder.Domain.Entities.Database;

namespace Reminder.Application.Services;

public class DisposablePromptService : IDisposablePromptService
{
    private readonly IApplicationDbContext _context;
    public DisposablePromptService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<long>> GetUserIdByPrompt(long disposablePromptId)
    {
        var disposablePrompt = await _context.DisposablePrompts.FirstOrDefaultAsync(dp => dp.Id == disposablePromptId);

        return disposablePrompt is not null
            ? Result<long>.Success(disposablePrompt.UserId)
            : Result<long>.Error(ErrorCode.DisposablePromptNotFound);
    }

    public async Task<Result<IEnumerable<DisposablePrompt>>> GetAllByUserId(long userId)
    {
        var disposablePrompts =
            await _context.DisposablePrompts.Where(dp => dp.UserId == userId).ToListAsync();
        
        return Result<IEnumerable<DisposablePrompt>>.Success(disposablePrompts);
    }

    public async Task<Result<DisposablePrompt>> Create(long userId, string title, string? description, DateTime showsAt)
    {
        if (showsAt < DateTime.Now)
            return Result<DisposablePrompt>.Error(ErrorCode.DisposablePromptBadShowTime);

        var newDisposablePrompt = new DisposablePrompt
        {
            UserId = userId,
            Title = title,
            Description = description,
            ShowsAt = showsAt
        };

        _context.DisposablePrompts.Add(newDisposablePrompt);
        await _context.SaveChangesAsync();

        return Result<DisposablePrompt>.Success(newDisposablePrompt);
    }

    public async Task<Result<None>> DeleteById(long disposablePromptId)
    {
        var disposablePrompt = await _context.DisposablePrompts.FirstOrDefaultAsync(dp => dp.Id == disposablePromptId);

        if (disposablePrompt is null)
            return Result<None>.Error(ErrorCode.DisposablePromptNotFound);

        _context.DisposablePrompts.Remove(disposablePrompt);
        await _context.SaveChangesAsync();

        return Result<None>.Success();
    }

    public async Task<Result<DisposablePrompt>> Update(long disposablePromptId, string title, string? description, DateTime showsAt)
    {
        var disposablePrompt = await _context.DisposablePrompts.AsTracking().FirstOrDefaultAsync(dp => dp.Id == disposablePromptId);
        
        if(disposablePrompt is null)
            return Result<DisposablePrompt>.Error(ErrorCode.DisposablePromptNotFound);
        
        if (showsAt < DateTime.Now)
            return Result<DisposablePrompt>.Error(ErrorCode.DisposablePromptBadShowTime);

        disposablePrompt.Title = title;
        disposablePrompt.Description = description;
        disposablePrompt.ShowsAt = showsAt;
        disposablePrompt.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Result<DisposablePrompt>.Success(disposablePrompt);
    }
}