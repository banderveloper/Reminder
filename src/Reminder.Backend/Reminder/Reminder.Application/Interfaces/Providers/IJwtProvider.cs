using Microsoft.IdentityModel.Tokens;
using Reminder.Domain.Enums;

namespace Reminder.Application.Interfaces.Providers;

public interface IJwtProvider
{
    /// <summary>
    /// Validation parameters with valid issuer, audience, lifetime, secret key...
    /// </summary>
    TokenValidationParameters ValidationParameters { get; }

    /// <summary>
    /// Extract userId from refresh token
    /// </summary>
    /// <param name="refreshToken">JWT refresh token</param>
    /// <returns>User id from refresh token</returns>
    long GetUserIdFromRefreshToken(string refreshToken);
    
    /// <summary>
    /// Generate user JWT, based on token validation parameters
    /// </summary>
    /// <param name="userId">Id of user, owner of JWT token</param>
    /// <param name="jwtType">Access/Refresh</param>
    /// <returns></returns>
    string GenerateUserJwt(long userId, JwtType jwtType);
    
    /// <summary>
    /// Check is refresh token valid using token validation parameters
    /// </summary>
    /// <param name="refreshToken">JWT refresh token to validate</param>
    /// <returns>JWT refresh token validity flag</returns>
    bool IsRefreshTokenValid(string refreshToken);
}