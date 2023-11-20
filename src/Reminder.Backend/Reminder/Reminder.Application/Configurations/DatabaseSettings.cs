namespace Reminder.Application.Configurations;

public class DatabaseSettings
{
    public static readonly string ConfigurationKey = "Database";
    
    public string ConnectionStringPattern { get; set; }
    
    public string Username { get; set; }
    public string Password { get; set; }
}