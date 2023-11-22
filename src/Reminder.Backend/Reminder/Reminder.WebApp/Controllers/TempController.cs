using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Reminder.WebApp.Controllers;

[Authorize]
[Route("temp")]
public class TempController : BaseController
{
    [HttpGet("get")]
    public string Get()
    {
        return "User id: " + UserId.ToString();
    }
    
    
}