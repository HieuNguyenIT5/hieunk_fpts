using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;

namespace RedisDemo.Controllers
{
    [Route("api/[controller]")]
    public class RedisDemoController : Controller
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ConnectionMultiplexer _redis;

        public RedisDemoController(IDistributedCache distributedCache, ConnectionMultiplexer redis)
        {
            _distributedCache = distributedCache;
            _redis = redis;
        }

        [HttpGet]
        public string Get()
        {
            var cacheKey = "name";
            var cached = _distributedCache.GetString(cacheKey);
            if (string.IsNullOrEmpty(cached))
            {
                var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(5));
                _distributedCache.SetString(cacheKey, "Nguyễn Khắc Hiếu", options);
                cached = _distributedCache.GetString(cacheKey);
            }
            var result = $"Cached: {cached}";
            return result;
        }
    }
} 