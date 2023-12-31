﻿using Microsoft.AspNetCore.Http;
using Reminder.Application.Configurations;
using Reminder.Application.DTOs;
using Reminder.Application.Interfaces.Providers;

namespace Reminder.Application.Providers;

public class CookieProvider : ICookieProvider
{
    private readonly CookiesConfiguration _cookieConfiguration;
    private readonly JwtConfiguration _jwtConfiguration;
    private readonly RefreshSessionConfiguration _refreshSessionConfiguration;

    public CookieProvider(CookiesConfiguration cookieConfiguration, RefreshSessionConfiguration refreshSessionConfiguration,
        JwtConfiguration jwtConfiguration)
    {
        _cookieConfiguration = cookieConfiguration;
        _refreshSessionConfiguration = refreshSessionConfiguration;
        _jwtConfiguration = jwtConfiguration;
    }
    
    public void AddAuthenticateCookiesToResponse(HttpResponse response, string accessToken, string refreshToken)
    {
        response.Cookies.Append(_cookieConfiguration.RefreshTokenCookieName, refreshToken,
            new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.Lax,
                Expires = new DateTimeOffset(DateTime.UtcNow.AddMinutes(_refreshSessionConfiguration.ExpirationMinutes))
            });

        response.Cookies.Append(_cookieConfiguration.AccessTokenCookieName, accessToken,
            new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.Lax,
                Expires = new DateTimeOffset(DateTime.UtcNow.AddMinutes(_jwtConfiguration.AccessExpirationMinutes))
            });
    }

    public void AddFingerprintCookieToResponse(HttpResponse response, string fingerprint)
    {
        response.Cookies.Append(_cookieConfiguration.FingerprintCookieName, fingerprint,
            new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.Lax,
                Expires = new DateTimeOffset(DateTime.UtcNow.AddMinutes(_refreshSessionConfiguration.ExpirationMinutes))
            });
    }

    public AccessRefreshTokensDTO GetAuthenticateTokensFromCookies(HttpRequest request)
    {
        request.Cookies.TryGetValue(_cookieConfiguration.AccessTokenCookieName, out var accessToken);
        request.Cookies.TryGetValue(_cookieConfiguration.RefreshTokenCookieName, out var refreshToken);

        return new AccessRefreshTokensDTO
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public string? GetFingerprintFromCookies(HttpRequest request)
    {
        request.Cookies.TryGetValue(_cookieConfiguration.FingerprintCookieName, out var fingerprint);
        return fingerprint;
    }

    public void DeleteCookiesFromResponse(HttpResponse response)
    {
        response.Cookies.Delete(_cookieConfiguration.FingerprintCookieName);
        response.Cookies.Delete(_cookieConfiguration.RefreshTokenCookieName);
        response.Cookies.Delete(_cookieConfiguration.AccessTokenCookieName);
    }
}