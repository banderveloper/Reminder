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
    public TokenValidationParameters ValidationParameters { get; }

    public JwtProvider(JwtConfiguration jwtConfiguration, RefreshSessionConfiguration refreshSessionConfiguration)
    {
        _jwtConfiguration = jwtConfiguration;
        _refreshSessionConfiguration = refreshSessionConfiguration;

        ValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _jwtConfiguration.Issuer,
            ValidateAudience = true,
            ValidAudience = _jwtConfiguration.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.SecretKey)),
        };
    }

    public string GenerateUserJwt(long userId, JwtType jwtType)
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

    public bool IsRefreshTokenValid(string refreshToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        ClaimsPrincipal? principal;
        
        try
        {
            principal = tokenHandler.ValidateToken(refreshToken, ValidationParameters, out _);
        }
        catch (Exception)
        {
            return false;
        }

        var tokenTypeClaim = principal.Claims.FirstOrDefault(claim => claim.Type.Equals(JwtRegisteredClaimNames.Typ));
        var tokenTypeValue = tokenTypeClaim?.Value;

        return tokenTypeValue != null && tokenTypeValue.Equals(JwtType.Refresh.ToString().ToLower());
    }

    public long GetUserIdFromRefreshToken(string refreshToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var claims = tokenHandler.ReadJwtToken(refreshToken).Claims;

        var userIdString = claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.NameIdentifier)).Value;

        return long.Parse(userIdString);
    }
}