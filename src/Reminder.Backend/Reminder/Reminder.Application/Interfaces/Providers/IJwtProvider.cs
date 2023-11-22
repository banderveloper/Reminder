using Microsoft.IdentityModel.Tokens;
using Reminder.Domain.Enums;

namespace Reminder.Application.Interfaces.Providers;

public interface IJwtProvider
{
    TokenValidationParameters ValidationParameters { get; }

    long GetUserIdFromRefreshToken(string refreshToken);
    
    string GenerateUserJwt(long userId, JwtType jwtType);
    
    bool IsRefreshTokenValid(string refreshToken);
}