using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reminder.Application;
using Reminder.Application.Interfaces.Providers;
using Reminder.Application.Interfaces.Services;
using Reminder.Domain.Enums;
using Reminder.WebApp.Models.Auth;

namespace Reminder.WebApp.Controllers;

[Route("auth")]
[AllowAnonymous]
public class AuthController : BaseController
{
    private readonly IUserService _userService;
    private readonly IRefreshSessionService _refreshSessionService;
    private readonly ICookieProvider _cookieProvider;
    private readonly IJwtProvider _jwtProvider;

    public AuthController(IUserService userService, ICookieProvider cookieProvider, IJwtProvider jwtProvider, IRefreshSessionService refreshSessionService)
    {
        _userService = userService;
        _cookieProvider = cookieProvider;
        _jwtProvider = jwtProvider;
        _refreshSessionService = refreshSessionService;
    }

    [HttpPost("sign-in")]
    public async Task<Result<None>> SignIn([FromBody] SignInRequestModel model)
    {
        // ALGORITHM:
        // Get user by credentials, check existing, generate tokens, create/update session, add tokens and fingerprint to response cookies
        
        var getUserResult = await _userService.GetByCredentialsAsync(model.Username, model.Password);
        
        if (!getUserResult.Succeed)
            return Result<None>.Error(getUserResult.ErrorCode);

        var user = getUserResult.Data;

        var accessToken = _jwtProvider.GenerateUserJwt(user.Id, JwtType.Access);
        var refreshToken = _jwtProvider.GenerateUserJwt(user.Id, JwtType.Refresh);

        await _refreshSessionService.CreateOrUpdateAsync(user.Id, model.Fingerprint, refreshToken);
        
        _cookieProvider.AddAuthenticateCookiesToResponse(HttpContext.Response, accessToken, refreshToken);
        _cookieProvider.AddFingerprintCookieToResponse(HttpContext.Response, model.Fingerprint);

        return Result<None>.Success();
    }

    [HttpPost("sign-up")]
    public async Task<Result<None>> SignUp([FromBody] SignUpRequestModel model)
    {
        // ALGORITHM:
        // Try to create user, check success, generate tokens, start session, add tokens and fingerprint to response cookies
        
        var createUserResult = await _userService.CreateAsync(model.Username, model.Password, model.Name);

        if (!createUserResult.Succeed)
            return Result<None>.Error(createUserResult.ErrorCode);

        var user = createUserResult.Data;

        var accessToken = _jwtProvider.GenerateUserJwt(user.Id, JwtType.Access);
        var refreshToken = _jwtProvider.GenerateUserJwt(user.Id, JwtType.Refresh);

        await _refreshSessionService.CreateOrUpdateAsync(user.Id, model.Fingerprint, refreshToken);
        
        _cookieProvider.AddAuthenticateCookiesToResponse(HttpContext.Response, accessToken, refreshToken);
        _cookieProvider.AddFingerprintCookieToResponse(HttpContext.Response, model.Fingerprint);

        return Result<None>.Success();
    }

    [HttpPost("refresh")]
    public async Task<Result<None>> Refresh()
    {
        // ALGORITHM:
        // Get refresh token and fingerprint from request cookies, check existing and validity, extract user id from refresh token...
        // ...check session existing, generate tokens, start session, add auth tokens to response cookies 
        
        var refreshToken = _cookieProvider.GetAuthenticateTokensFromCookies(HttpContext.Request).RefreshToken;
        var fingerprint = _cookieProvider.GetFingerprintFromCookies(HttpContext.Request);

        if (refreshToken is null)
            return Result<None>.Error(ErrorCode.RefreshCookieNotFound);
        if (fingerprint is null)
            return Result<None>.Error(ErrorCode.FingerprintCookieNotFound);

        if (!_jwtProvider.IsTokenValid(refreshToken, JwtType.Refresh))
            return Result<None>.Error(ErrorCode.BadRefreshToken);

        var userId = _jwtProvider.GetUserIdFromToken(refreshToken);

        var sessionExistsResult = await _refreshSessionService.SessionKeyExistsAsync(userId, fingerprint);
        var sessionExists = sessionExistsResult.Data;

        if (!sessionExists)
            return Result<None>.Error(ErrorCode.SessionNotFound);
        
        var accessToken = _jwtProvider.GenerateUserJwt(userId, JwtType.Access);
        refreshToken = _jwtProvider.GenerateUserJwt(userId, JwtType.Refresh);

        await _refreshSessionService.CreateOrUpdateAsync(userId, fingerprint, refreshToken);
        
        _cookieProvider.AddAuthenticateCookiesToResponse(HttpContext.Response, accessToken, refreshToken);
        
        return Result<None>.Success();
    }

    [HttpPost("sign-out")]
    public async Task<Result<None>> Logout()
    {
        // ALGORITHM:
        // Get refresh token and fingerprint from request cookies, check their existing and token validity...
        // extract user id from refresh token, delete session, delete cookies
        
        var refreshToken = _cookieProvider.GetAuthenticateTokensFromCookies(HttpContext.Request).RefreshToken;
        var fingerprint = _cookieProvider.GetFingerprintFromCookies(HttpContext.Request);

        if (refreshToken is null)
            return Result<None>.Error(ErrorCode.RefreshCookieNotFound);
        if (fingerprint is null)
            return Result<None>.Error(ErrorCode.FingerprintCookieNotFound);

        if (!_jwtProvider.IsTokenValid(refreshToken, JwtType.Refresh))
            return Result<None>.Error(ErrorCode.BadRefreshToken);

        var userId = _jwtProvider.GetUserIdFromToken(refreshToken);
        
        await _refreshSessionService.DeleteAsync(userId, fingerprint);
        
        _cookieProvider.DeleteCookiesFromResponse(HttpContext.Response);

        return Result<None>.Success();
    }
}