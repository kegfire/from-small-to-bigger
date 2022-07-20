using ServiceStack.Redis;

namespace SomeService
{
    public class RedisCacheService : ICacheService
    {
        private readonly IRedisClientsManager _redisClientsManager;

        public RedisCacheService(IRedisClientsManager redisClientsManager)
        {
            _redisClientsManager = redisClientsManager;
        }

        public async Task<T> GetValueAsync<T>(string id)
        {
            await using var redisClient = await _redisClientsManager.GetReadOnlyClientAsync();
            return await redisClient.GetAsync<T>(id);
        }

        public async Task SetValueAsync<T>(string key, T value)
        {
            await using var redisClient = await _redisClientsManager.GetClientAsync();
            await redisClient.AddAsync(key, value);
        }

        public async Task DeleteValueAsync<T>(string id)
        {
            await using var redisClient = await _redisClientsManager.GetClientAsync();
            var value = await redisClient.GetAsync<T>(id);
            if (value != null)
            {
                await redisClient.DeleteAsync(value);
            }
        }

        public async Task UpdateValueAsync<T>(string id, T value)
        {
            await using var redisClient = await _redisClientsManager.GetClientAsync();
            await redisClient.ReplaceAsync(id, value);
        }
    }
}
