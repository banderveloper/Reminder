namespace Reminder.Application.Configurations;

public class RedisSettings
{
    public static readonly string ConfigurationKey = "Redis";
    
    public string ConnectionString { get; set; }
}