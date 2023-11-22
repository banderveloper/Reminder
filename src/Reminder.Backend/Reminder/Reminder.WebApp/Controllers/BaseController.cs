using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Reminder.WebApp.Controllers;

// Parent class for all controllers
[ApiController]
[Route("[controller]/[action]")]
public class BaseController : ControllerBase
{
    // UserId got from access token
    // Access token stored in request cookie but middleware copy it to Authenticate header
    internal long UserId => long.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
}