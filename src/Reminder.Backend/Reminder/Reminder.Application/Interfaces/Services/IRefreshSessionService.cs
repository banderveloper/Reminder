using Reminder.Application.DTOs;

namespace Reminder.Application.Interfaces.Services;

public interface IRefreshSessionService
{
    Task<Result<None>> CreateOrUpdateSessionAsync(long userId, string fingerprint, string refreshToken);
    Task<Result<None>> DeleteSessionAsync(long userId, string fingerprint);
}