using Grpc.Core;
using SocialService.Application.Interfaces;
using SocialService.Domain.Entities;
using SoundAlone.Contracts.SocialService.V1;

namespace SocialService.Api.Grpc;

public class UserProfileGrpcService : SoundAlone.Contracts.SocialService.V1.SocialService.SocialServiceBase
{
    private IUserProfileService _userProfileService;

    public UserProfileGrpcService(IUserProfileService userProfileService)
    {
        _userProfileService = userProfileService;
    }

    public override async Task<CreateProfileResponse> CreateProfile(CreateProfileRequest request, ServerCallContext context)
    {
        try
        {
            var profileId = await _userProfileService.CreateUserProfile(new UserProfile()
            {
                Id = Guid.NewGuid()
            });
            return new CreateProfileResponse
            {
                ProfileId = profileId.ToString()
            };
        }
        catch (Exception e)
        {
            throw new RpcException(new Status(StatusCode.Internal, e.Message));
        }
    }
}