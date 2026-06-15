using Investry.Application.Contracts.Infrastructure;
using MediatR;

namespace Investry.Application.Behaviors.Caching
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ICacheService _cache;

        public CachingBehavior(ICacheService cache)
        {
            _cache = cache;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is not ICacheableQuery<TResponse> cacheable)
                return await next();

            var cachedData = await _cache.GetAsync<TResponse>(cacheable.CacheKey);

            if (cachedData is not null)
                return cachedData;

            var response = await next();

            await _cache.SetAsync(cacheable.CacheKey, response, TimeSpan.FromMinutes(cacheable.ExpirationMinutes));

            return response;
        }
    }
}
