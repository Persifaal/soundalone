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
    private ILogger<UserProfileGrpcService> _logger;

    public UserProfileGrpcService(IUserProfileService userProfileService, ILogger<UserProfileGrpcService> logger)
    {
        _userProfileService = userProfileService;
        _logger = logger;
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

    public override async Task<GetProfileResponse> GetProfile(GetProfileRequest request, ServerCallContext context)
    {
        try
        {
            var profile = await _userProfileService.GetUserProfileById(Guid.Parse(request.UserId));
            if(profile == null)
                throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
            var response = new GetProfileResponse
            {
                Profile =
                {
                    AvatarUrl = profile.AvatarUrl,
                    BannerUrl = profile.BannerUrl,
                    Bio = profile.Bio,
                    DisplayName = profile.DisplayName,
                    UserTag = profile.UserTag
                }
            };

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message + "\n" + e.StackTrace);
            throw new RpcException(new Status(StatusCode.Internal, e.Message));
        }
    }

    public override async Task<Profile> GetMyProfile(GetMyProfileRequest request, ServerCallContext context)
    {
        try
        {
            UserProfile myProfile = await _userProfileService.GetMe(Guid.Parse(request.AccountId));
            return new Profile()
            {
                DisplayName = myProfile.DisplayName ?? string.Empty,
                UserTag = myProfile.UserTag ?? string.Empty,
                AvatarUrl = myProfile.AvatarUrl,
                BannerUrl = myProfile.BannerUrl,
                Bio = myProfile.Bio
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message + "\n" + e.StackTrace);
            throw new RpcException(new Status(StatusCode.Internal, e.Message));
        }
    }
}