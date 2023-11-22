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
    /// Extract userId from jwt token
    /// </summary>
    /// <param name="jwtToken">JWT token</param>
    /// <returns>User id from token</returns>
    long GetUserIdFromToken(string jwtToken);
    
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