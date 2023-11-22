namespace Reminder.Domain.Enums;

// Access and refresh tokens are both JWT, so JWT contains key TYPE (access/refresh)
public enum JwtType
{
    Access,
    Refresh
}