using Investry.Application.Common;
using Investry.Application.Contracts.Identity;
using Investry.Application.Contracts.Infrastructure;
using Investry.Domain.Enums;
using MediatR;

namespace Investry.Application.Features.Users.Commands.UploadProfilePicture
{
    public class UploadProfilePictureHandler : IRequestHandler<UploadProfilePictureCommand, Result<string>>
    {
        private readonly IIdentityService _identityService;
        private readonly IMediaService _mediaService;
        private readonly ICacheService _cache;

        public UploadProfilePictureHandler(IIdentityService identityService, IMediaService mediaService, ICacheService cache)
        {
            _identityService = identityService;
            _mediaService = mediaService;
            _cache = cache;
        }
        public async Task<Result<string>> Handle(UploadProfilePictureCommand request, CancellationToken cancellationToken)
        {
            var user = await _identityService.GetUserByIdAsync(request.UserId);
            if (user is null)
                return Result<string>.Failure(new List<Error> { new Error("User.NotFound", "User not found", ErrorType.NotFound) });
            
            if(!string.IsNullOrEmpty(user.ProfilePicturePublicId))
                await _mediaService.DeleteMediaAsync(user.ProfilePicturePublicId, MediaType.Image);

            var (url, publicId, type) = await _mediaService.AddMediaAsync(request.ProfilePicture);

            var updateResult = await _identityService.UpdateUserProfilePictureAsync(request.UserId, url, publicId);

            if (!updateResult)
                return Result<string>.Failure(new List<Error> { new Error("User.UpdateFailed", "Failed to update user profile picture", ErrorType.Failure) });

            await _cache.RemoveAsync($"profile-{request.UserId}");

            return Result<string>.Success(url);
        }
    }
}
