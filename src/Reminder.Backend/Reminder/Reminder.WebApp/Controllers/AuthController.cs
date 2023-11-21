using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Reminder.Application;
using Reminder.Application.Interfaces.Services;
using Reminder.Application.Providers;
using Reminder.Domain.Enums;
using Reminder.WebApp.Models.Auth;

namespace Reminder.WebApp.Controllers;

[Route("auth")]
public class AuthController : BaseController
{
    private readonly IUserService _userService;
    private readonly IRefreshSessionService _refreshSessionService;
    private readonly CookieProvider _cookieProvider;
    private readonly JwtProvider _jwtProvider;

    public AuthController(IUserService userService, CookieProvider cookieProvider, JwtProvider jwtProvider, IRefreshSessionService refreshSessionService)
    {
        _userService = userService;
        _cookieProvider = cookieProvider;
        _jwtProvider = jwtProvider;
        _refreshSessionService = refreshSessionService;
    }

    [HttpPost("sign-in")]
    public async Task<Result<None>> SignIn([FromBody] SignInRequestModel model)
    {
        var getUserResult = await _userService.GetUserByCredentialsAsync(model.Username, model.Password);
        
        if (!getUserResult.Succeed)
            return Result<None>.Error(getUserResult.ErrorCode);

        var user = getUserResult.Data;

        var accessToken = _jwtProvider.GenerateUserJwt(user.Id, JwtType.Access);
        var refreshToken = _jwtProvider.GenerateUserJwt(user.Id, JwtType.Refresh);

        await _refreshSessionService.CreateOrUpdateSessionAsync(user.Id, model.Fingerprint, refreshToken);
        
        _cookieProvider.AddAuthenticateCookiesToResponse(HttpContext.Response, accessToken, refreshToken);

        return Result<None>.Success(new None());
    }

    [HttpPost("sign-up")]
    public async Task<Result<None>> SignUp([FromBody] SignUpRequestModel model)
    {
        var createUserResult = await _userService.CreateUserAsync(model.Username, model.Password, model.Name);

        if (!createUserResult.Succeed)
            return Result<None>.Error(createUserResult.ErrorCode);

        var user = createUserResult.Data;

        var accessToken = _jwtProvider.GenerateUserJwt(user.Id, JwtType.Access);
        var refreshToken = _jwtProvider.GenerateUserJwt(user.Id, JwtType.Refresh);

        await _refreshSessionService.CreateOrUpdateSessionAsync(user.Id, model.Fingerprint, refreshToken);
        
        _cookieProvider.AddAuthenticateCookiesToResponse(HttpContext.Response, accessToken, refreshToken);

        return Result<None>.Success(new None());
    }
}