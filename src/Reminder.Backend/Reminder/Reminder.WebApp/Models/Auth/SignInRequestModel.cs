namespace Reminder.WebApp.Models.Auth;

// todo validation
public class SignInRequestModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Fingerprint { get; set; }
}