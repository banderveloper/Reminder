namespace Reminder.Domain.Entities.Database;

public class User : DatabaseEntity
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string? Name { get; set; }
}