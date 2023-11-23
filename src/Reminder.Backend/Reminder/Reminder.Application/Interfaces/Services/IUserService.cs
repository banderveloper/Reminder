using Reminder.Domain.Entities.Database;

namespace Reminder.Application.Interfaces.Services;

/// <summary>
/// Service used for handling users in database
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Create new user instance
    /// </summary>
    /// <param name="username">Login/username of new user</param>
    /// <param name="password">Password before hashing</param>
    /// <param name="name">Personal name (can be null)</param>
    /// <returns>Result with created user, or error code</returns>
    Task<Result<User>> CreateAsync(string username, string password, string? name);
    
    /// <summary>
    /// Get user by id
    /// </summary>
    /// <param name="id">User id</param>
    /// <returns>Result with found user, or error code</returns>
    Task<Result<User>> GetByIdAsync(long id);
    
    /// <summary>
    /// Get user by username and password
    /// </summary>
    /// <param name="username">Login/username of user</param>
    /// <param name="password">Password before hashing</param>
    /// <returns>Result with found user, or error code</returns>
    Task<Result<User>> GetByCredentialsAsync(string username, string password);
}