using API.SEG.Aplicacion.Interface.Infraestructure;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text;
using StackExchange.Redis;

namespace API.SEG.Infraestructure.Services
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache _distributedCache;

        public RedisCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task SetAsync<T>(string key, T value, int absoluteExpiration, int slidingExpiration)
        {
            try
            {
                var serializeData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value));
                var options = new DistributedCacheEntryOptions()
                            .SetAbsoluteExpiration(TimeSpan.FromMinutes(absoluteExpiration))
                            .SetSlidingExpiration(TimeSpan.FromMinutes(slidingExpiration));

                await _distributedCache.SetAsync(key, serializeData, options);
            }
            catch (RedisException ex)
            {
                throw new RedisException("Se produjo un error inesperado al configurar el valor en caché para la clave {Key}", ex);
            }
        }

        public async Task<T> GetAsync<T>(string key)
        {
            try
            {
                var result = await _distributedCache.GetAsync(key);

                if (result == null)
                    throw new RedisCommandException("Ocurrió un error inesperado. No se encontro el valor en la caché");

                return JsonSerializer.Deserialize<T>(result);
            }
            catch (RedisException ex)
            {
                throw new RedisException("Se produjo un error inesperado al obtener el valor en caché para la clave {Key}", ex);
            }
        }
    }
}
