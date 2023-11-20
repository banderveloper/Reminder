namespace Reminder.Application.Configurations;

public class CookiesConfiguration
{
    public static readonly string ConfigurationKey = "Cookies";
    
    public string AccessTokenCookieName { get; set; }
    public string RefreshTokenCookieName { get; set; }
}