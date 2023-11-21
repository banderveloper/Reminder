using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Reminder.WebApp.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class BaseController : ControllerBase
{
    internal int? UserId => User.Identity.IsAuthenticated
        ? int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
        : null;
}