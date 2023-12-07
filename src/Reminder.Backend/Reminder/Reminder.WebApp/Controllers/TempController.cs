using Microsoft.AspNetCore.Authorization;
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

    [HttpGet("cookie")]
    public IActionResult CookieTemp()
    {
        Console.WriteLine("GOT COOKIES FROM REQUEST: ");
        foreach (var cookie in HttpContext.Request.Cookies)
        {
            Console.WriteLine($"Key: {cookie.Key} // Value: {cookie.Value}");
        }
        
        HttpContext.Response.Cookies.Append("temp-cookie", "hello-world");

        return Ok();
    }

    [Authorize]
    [HttpGet("protected")]
    public IActionResult Protected()
    {
        Console.WriteLine("GOT TO PROTECTED USER ID" + UserId);
        return Ok(UserId);
    }
}