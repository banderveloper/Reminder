using Microsoft.AspNetCore.Http;
using Reminder.Application.DTOs;

namespace Reminder.Application.Interfaces.Providers;

public interface ICookieProvider
{
    void AddAuthenticateCookiesToResponse(HttpResponse response, string accessToken, string refreshToken);
    void AddFingerprintCookieToResponse(HttpResponse response, string fingerprint);
    
    AccessRefreshTokensDTO GetAuthenticateTokensFromCookies(HttpRequest request);
    string? GetFingerprintFromCookies(HttpRequest request);

    void DeleteCookiesFromResponse(HttpResponse response);
}