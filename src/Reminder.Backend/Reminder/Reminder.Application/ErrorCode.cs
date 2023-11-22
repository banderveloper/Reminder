using System.Text.Json.Serialization;
using Reminder.Application.Converters;

namespace Reminder.Application;

// Result error code for responses
[JsonConverter(typeof(SnakeCaseStringEnumConverter<ErrorCode>))]
public enum ErrorCode
{
    Unknown,
    
    UsernameAlreadyExists,
    UserNotFound,
    
    FingerprintCookieNotFound,
    RefreshCookieNotFound,
    
    BadRefreshToken,
    
    SessionNotFound
}