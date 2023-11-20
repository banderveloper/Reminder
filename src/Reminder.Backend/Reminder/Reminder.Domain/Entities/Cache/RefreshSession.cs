namespace Reminder.Domain.Entities.Cache;

public class RefreshSession : CacheEntity
{
    public long UserId { get; set; }
    public string Fingerprint { get; set; }
    public string RefreshToken { get; set; }
    public static string GetCacheKey(long userId, string fingerprint) => $"{userId}::{fingerprint}";
}