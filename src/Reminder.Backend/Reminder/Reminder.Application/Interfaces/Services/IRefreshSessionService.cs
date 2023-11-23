namespace Reminder.Application.Interfaces.Services;

// Service used for handling refresh sessions
public interface IRefreshSessionService
{
    /// <summary>
    /// Create new session, or update existing
    /// </summary>
    /// <param name="userId">User id</param>
    /// <param name="fingerprint">Unique client code</param>
    /// <param name="refreshToken">JWT refresh token</param>
    /// <returns></returns>
    Task<Result<None>> CreateOrUpdateAsync(long userId, string fingerprint, string refreshToken);
    
    /// <summary>
    /// End user session
    /// </summary>
    /// <param name="userId">User id</param>
    /// <param name="fingerprint">Unique client code</param>
    /// <returns></returns>
    Task<Result<None>> DeleteAsync(long userId, string fingerprint);
    
    /// <summary>
    /// Check session existance
    /// </summary>
    /// <param name="userId">User id</param>
    /// <param name="fingerprint">Unique client code</param>
    /// <returns></returns>
    Task<Result<bool>> SessionKeyExistsAsync(long userId, string fingerprint);
}