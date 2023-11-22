using Microsoft.AspNetCore.Http;
using Reminder.Application.DTOs;

namespace Reminder.Application.Interfaces.Providers;

public interface ICookieProvider
{
    /// <summary>
    /// Add access and refresh token to response cookies
    /// </summary>
    /// <param name="response">HttpContext response, sent to client</param>
    /// <param name="accessToken">JWT Access token</param>
    /// <param name="refreshToken">JWT refresh token</param>
    void AddAuthenticateCookiesToResponse(HttpResponse response, string accessToken, string refreshToken);
    
    /// <summary>
    /// Add fingerprint (unique client code) to response cookies 
    /// </summary>
    /// <param name="response">HttpContext response, sent to client</param>
    /// <param name="fingerprint">Fingerprint (unique client code)</param>
    void AddFingerprintCookieToResponse(HttpResponse response, string fingerprint);
    
    /// <summary>
    /// Extract access and refresh tokens from request cookies
    /// </summary>
    /// <param name="request">HttpContext request, sent from client</param>
    /// <returns>Union of access and refresh token</returns>
    AccessRefreshTokensDTO GetAuthenticateTokensFromCookies(HttpRequest request);
    
    /// <summary>
    /// Extract fingerprint from request cookies
    /// </summary>
    /// <param name="request">HttpContext request, sent from client</param>
    /// <returns>Fingerprint from request cookies (unique client code)</returns>
    string? GetFingerprintFromCookies(HttpRequest request);

    /// <summary>
    /// Delete (expire) cookies with access+refresh tokens, and fingerprint
    /// </summary>
    /// <param name="response">HttpContext response, sent to client</param>
    void DeleteCookiesFromResponse(HttpResponse response);
}