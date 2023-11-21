using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Reminder.Application.Configurations;
using Reminder.Domain.Enums;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Reminder.Application.Providers;

public class JwtProvider
{
    private readonly JwtConfiguration _jwtConfiguration;
    private readonly RefreshSessionConfiguration _refreshSessionConfiguration;

    public JwtProvider(JwtConfiguration jwtConfiguration, RefreshSessionConfiguration refreshSessionConfiguration)
    {
        _jwtConfiguration = jwtConfiguration;
        _refreshSessionConfiguration = refreshSessionConfiguration;
    }

    public string GenerateUserJwt(int userId, JwtType jwtType)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(JwtRegisteredClaimNames.Typ, jwtType.ToString().ToLower())
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expirationTime = jwtType switch
        {
            JwtType.Access => DateTime.UtcNow.AddMinutes(_jwtConfiguration.AccessExpirationMinutes),
            JwtType.Refresh => DateTime.UtcNow.AddMinutes(_refreshSessionConfiguration.ExpirationMinutes),
            _ => throw new ArgumentOutOfRangeException(nameof(jwtType), jwtType, "Unknown jwt type enum value")
        };

        // create token and return it
        var token = new JwtSecurityToken(
            issuer: _jwtConfiguration.Issuer,
            audience: _jwtConfiguration.Audience,
            claims: claims,
            expires: expirationTime,
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}