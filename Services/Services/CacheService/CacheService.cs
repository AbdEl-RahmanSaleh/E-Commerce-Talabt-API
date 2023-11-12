using StackExchange.Redis;
using System.Text.Json;

namespace Services.Services.CacheService
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;
        public CacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task SetCascheResponseAsync(string casheKey, object response, TimeSpan timeToLive)
        {
            if (response is null)
                return;
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var serializedResponse = JsonSerializer.Serialize(response, options); 

            await _database.StringSetAsync(casheKey, serializedResponse,timeToLive);
        }

        public async Task<string> GetCascheResponseAsync(string casheKey)
        { 
            var cachedResponse = await _database.StringGetAsync(casheKey);

            if (cachedResponse.IsNullOrEmpty)
                return null;

            return cachedResponse;
        }

    }
}
