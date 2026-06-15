using MediatR;

namespace Investry.Application.Behaviors.Caching
{
    public interface ICacheableQuery<TResponse> : IRequest<TResponse>
    {
        string CacheKey { get; }
        int ExpirationMinutes { get; }
    }
}
