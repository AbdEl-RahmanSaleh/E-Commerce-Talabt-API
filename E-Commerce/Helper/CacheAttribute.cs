using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.Services.CacheService;
using System.Text;

namespace E_Commerce.Helper
{
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveInSeconds;

        public CacheAttribute(int timeToLiveInSeconds)
        {
            _timeToLiveInSeconds = timeToLiveInSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService =  context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

            var cachedResponse = await cacheService.GetCascheResponseAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = contentResult;

                return;
            }

            var executedContect = await next();

            if (executedContect.Result is OkObjectResult response) 
            {
                await cacheService.SetCascheResponseAsync(cacheKey,response.Value,TimeSpan.FromSeconds(_timeToLiveInSeconds));
            }

        }


        private string GenerateCacheKeyFromRequest(HttpRequest request) 
        {
            var cacheKey = new StringBuilder();

            cacheKey.Append($"{request.Path}");

            foreach(var (key, value) in request.Query.OrderBy(x =>x.Key)) //tuple
            {
                cacheKey.Append($"|{key}-{value}");
            }

            return cacheKey.ToString();
        }
    }
}
