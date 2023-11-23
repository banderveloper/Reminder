using Microsoft.AspNetCore.Mvc;

namespace Reminder.WebApp.Controllers;

[Route("temp")]
public class TempController : BaseController
{
    [HttpGet("get")]
    public string Get()
    {
        return "id: " + UserId;
    }
}