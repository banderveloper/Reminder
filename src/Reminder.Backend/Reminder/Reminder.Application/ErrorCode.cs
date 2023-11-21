namespace Reminder.Application;

public enum ErrorCode
{
    Unknown,
    
    UsernameAlreadyExists,
    UserNotFound,
    
    FingerprintCookieNotFound,
    RefreshCookieNotFound,
}