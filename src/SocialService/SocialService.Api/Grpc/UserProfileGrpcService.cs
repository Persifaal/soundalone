using Google.Protobuf;
using Google.Protobuf.Collections;
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

    public override async Task<UpdateProfileResponse> UpdateProfile(UpdateProfileRequest request, ServerCallContext context)
    {
        try
        {
            await _userProfileService.UpdateUserProfile(new UserProfile());
            return new UpdateProfileResponse();
        }
        catch (Exception e)
        {
            throw new RpcException(new Status(StatusCode.Internal, e.Message));
        }
    }

    public override async Task<GetProfilesResponse> GetProfiles(GetProfilesRequest request, ServerCallContext context)
    {
        try
        {
            var profiles = await _userProfileService.GetAllUserProfiles(
                request.Page,
                request.PageSize,
                request.SearchString
            );

            var response = new GetProfilesResponse();

            response.Profiles.AddRange(profiles.Select(p => new Profile()
            {
                DisplayName = p.DisplayName ?? string.Empty,
                UserTag = p.UserTag ?? string.Empty,
                AvatarUrl = p.AvatarUrl ?? string.Empty,
                BannerUrl = p.BannerUrl ?? string.Empty,
                Bio = p.Bio ?? string.Empty
            }));

            return response;
        }
        catch (Exception e)
        {
            throw new RpcException(new Status(StatusCode.Internal, e.Message));
        }
    }
}