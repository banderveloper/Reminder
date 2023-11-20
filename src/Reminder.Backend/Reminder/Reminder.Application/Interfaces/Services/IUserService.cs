using Reminder.Domain.Entities.Database;

namespace Reminder.Application.Interfaces.Services;

public interface IUserService
{
    Task<Result<User>> CreateUserAsync(string username, string password, string? name);
    Task<Result<User>> GetUserByIdAsync(long id);
    Task<Result<User>> GetUserByCredentialsAsync(string username, string password);
}