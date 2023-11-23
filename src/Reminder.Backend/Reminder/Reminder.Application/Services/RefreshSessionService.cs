using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Reminder.Application.Configurations;
using Reminder.Application.Interfaces.Services;
using Reminder.Domain.Entities.Cache;

namespace Reminder.Application.Services;

public class RefreshSessionService : IRefreshSessionService
{
    private readonly IDistributedCache _redis;
    private readonly RefreshSessionConfiguration _refreshSessionConfiguration;

    public RefreshSessionService(IDistributedCache redis, RefreshSessionConfiguration refreshSessionConfiguration)
    {
        _redis = redis;
        _refreshSessionConfiguration = refreshSessionConfiguration;
    }

    public async Task<Result<None>> CreateOrUpdateAsync(long userId, string fingerprint, string refreshToken)
    {
        var redisKey = RefreshSession.GetCacheKey(userId, fingerprint);

        var entity = new RefreshSession
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Fingerprint = fingerprint,
            RefreshToken = refreshToken,
            DestroysAt = DateTime.UtcNow + new TimeSpan(
                hours: 0,
                minutes: _refreshSessionConfiguration.ExpirationMinutes,
                seconds: 0)
        };
        var redisValue = JsonSerializer.Serialize(entity);

        await _redis.SetAsync(redisKey, Encoding.UTF8.GetBytes(redisValue),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_refreshSessionConfiguration.ExpirationMinutes)
            });

        return Result<None>.Success();
    }

    public async Task<Result<None>> DeleteAsync(long userId, string fingerprint)
    {
        var redisKey = RefreshSession.GetCacheKey(userId, fingerprint);
        await _redis.RemoveAsync(redisKey);

        return Result<None>.Success();
    }

    public async Task<Result<bool>> SessionKeyExistsAsync(long userId, string fingerprint)
    {
        var redisKey = RefreshSession.GetCacheKey(userId, fingerprint);

        var data = await _redis.GetAsync(redisKey);

        return Result<bool>.Success(data is not null);
    }
}