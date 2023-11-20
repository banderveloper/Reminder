namespace Reminder.Application.Configurations;

public class ConnectionsSettings
{
    public static readonly string ConfigurationKey = "ConnectionStrings";
    
    public string Sqlite { get; set; }
    public string Redis { get; set; }
}