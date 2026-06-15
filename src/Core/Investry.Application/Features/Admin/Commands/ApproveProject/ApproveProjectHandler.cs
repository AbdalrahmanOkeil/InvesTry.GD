using Investry.Application.Common;
using Investry.Application.Contracts.Infrastructure;
using Investry.Application.Contracts.Persistence;
using Investry.Domain.Enums;
using MediatR;

namespace Investry.Application.Features.Admin.Commands.ApproveProject
{
    public class ApproveProjectHandler
        : IRequestHandler<ApproveProjectCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cache;

        public ApproveProjectHandler(IUnitOfWork unitOfWork, ICacheService cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<Result<string>> Handle(
            ApproveProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.ProjectRepository.GetAsync(request.ProjectId);

            if (project is null)
                return Result<string>.Failure(new List<Error>
                {
                    new("Project.NotFound", "Project not found.", ErrorType.NotFound)
                });

            if (project.ProjectStatus != ProjectStatus.PendingReview)
                return Result<string>.Failure(new List<Error>
                {
                    new("Project.InvalidStatus",
                        $"Only projects with status '{nameof(ProjectStatus.PendingReview)}' can be approved. Current status: '{project.ProjectStatus}'.",
                        ErrorType.Validation)
                });

            project.ProjectStatus = ProjectStatus.Published;
            project.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.SaveAsync();

            await _cache.RemoveAsync("all-projects");
            await _cache.RemoveAsync($"project-details-{request.ProjectId}");

            return Result<string>.Success("Project approved successfully.");
        }
    }
}
