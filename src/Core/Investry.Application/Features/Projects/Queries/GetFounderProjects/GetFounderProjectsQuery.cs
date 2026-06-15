using Investry.Application.Behaviors.Caching;
using Investry.Application.Common;

namespace Investry.Application.Features.Projects.Queries.GetFounderProjects
{
    public record GetFounderProjectsQuery(string UserId) : ICacheableQuery<Result<IReadOnlyList<FounderProjectDto>>>
    {
        public string CacheKey => $"founder-projects-{UserId}";

        public int ExpirationMinutes => 10;
    }
}
