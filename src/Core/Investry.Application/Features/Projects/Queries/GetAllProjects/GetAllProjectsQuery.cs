using Investry.Application.Behaviors.Caching;
using Investry.Application.Common;

namespace Investry.Application.Features.Projects.Queries.GetAllProjects
{
    public record GetAllProjectsQuery() : ICacheableQuery<Result<IReadOnlyList<ProjectSummaryDto>>>
    {
        public string CacheKey => "all-projects";

        public int ExpirationMinutes => 5;
    }
}
