using System.Text.Json.Serialization;
using Reminder.Application.Converters;

namespace Reminder.Application;

// Result error code for responses
[JsonConverter(typeof(SnakeCaseStringEnumConverter<ErrorCode>))]
public enum ErrorCode
{
    Unknown,
    
    AuthenticationServiceUnavailable,
    
    UsernameAlreadyExists,
    UserNotFound,
    
    FingerprintCookieNotFound,
    RefreshCookieNotFound,
    
    BadRefreshToken,
    
    SessionNotFound,
    
    DisposablePromptNotFound,
    DisposablePromptBadShowTime,
    DisposablePromptProtected
}