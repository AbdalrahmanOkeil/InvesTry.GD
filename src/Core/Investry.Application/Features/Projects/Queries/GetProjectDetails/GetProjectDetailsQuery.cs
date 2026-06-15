using Investry.Application.Behaviors.Caching;
using Investry.Application.Common;
using Investry.Application.Contracts.Infrastructure;
using MediatR;

namespace Investry.Application.Features.Projects.Queries.GetProjectDetails
{
    public record GetProjectDetailsQuery(Guid ProjectId) : ICacheableQuery<Result<ProjectDetailsDto>>
    {
        public string CacheKey => $"project-details-{ProjectId}";

        public int ExpirationMinutes => 10;
    }
}
