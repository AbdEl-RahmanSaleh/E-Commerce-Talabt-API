using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.CacheService
{
    public interface ICacheService
    {
        Task SetCascheResponseAsync(string casheKey, object response , TimeSpan timeToLive);
        Task<string> GetCascheResponseAsync(string casheKey);

    }
}
