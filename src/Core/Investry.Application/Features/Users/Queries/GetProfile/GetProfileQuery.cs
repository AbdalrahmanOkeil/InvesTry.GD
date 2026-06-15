using Investry.Application.Behaviors.Caching;
using Investry.Application.Common;
using MediatR;

namespace Investry.Application.Features.Users.Queries.GetProfile
{
    public record GetProfileQuery(string UserId) : ICacheableQuery<Result<ProfileDto>>
    {
        public string CacheKey => $"profile-{UserId}";

        public int ExpirationMinutes => 20;
    }
}
