using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Reminder.Application.Interfaces;
using Reminder.Application.Interfaces.Services;
using Reminder.Domain.Entities.Database;

namespace Reminder.Application.Services;

public class UserService : IUserService
{
    private readonly IApplicationDbContext _context;

    public UserService(IApplicationDbContext context) =>
        _context = context;
    
    public async Task<Result<User>> CreateUserAsync(string username, string password, string? name)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(user => user.Username.Equals(username));

        if (existingUser is not null)
            return Result<User>.Error(ErrorCode.UsernameAlreadyExists);

        var newUser = new User
        {
            Username = username,
            Name = name,
            PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(password, HashType.SHA256)
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return Result<User>.Success(newUser);
    }

    public async Task<Result<User>> GetUserByIdAsync(long id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);

        return user is not null
            ? Result<User>.Success(user)
            : Result<User>.Error(ErrorCode.UserNotFound);
    }

    public async Task<Result<User>> GetUserByCredentialsAsync(string username, string password)
    {
        var hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password, HashType.SHA256);

        var user = await _context.Users.FirstOrDefaultAsync(user =>
            user.Username.Equals(username));

        return user is not null && user.PasswordHash.Equals(hashedPassword)
            ? Result<User>.Success(user)
            : Result<User>.Error(ErrorCode.UserNotFound);
    }
}