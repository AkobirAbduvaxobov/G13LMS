using Microsoft.Extensions.Caching.Distributed;

namespace LMSPro.Api.Services;

public class RedisCacheService
{
    private readonly IDistributedCache Cache;

    public RedisCacheService(IDistributedCache cache)
    {
        Cache = cache;
    }

    public async Task SetAsync(string key, string value, DistributedCacheEntryOptions distributedCacheEntryOptions = null)
    {
        await Cache.SetStringAsync(key, value, distributedCacheEntryOptions);
    }

    public async Task<string?> GetAsync(string key)
    {
        return await Cache.GetStringAsync(key);
    }

    public async Task RemoveAsync(string key)
    {
        await Cache.RemoveAsync(key);
    }
}
