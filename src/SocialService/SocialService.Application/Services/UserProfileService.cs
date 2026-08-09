using SocialService.Application.Interfaces;
using SocialService.Domain.Entities;

namespace SocialService.Application.Services;

public class UserProfileService : IUserProfileService
{
    private IUserProfileRepository _userProfileRepository;

    public UserProfileService(IUserProfileRepository repository)
    {
        _userProfileRepository = repository;
    }
    
    public Task<List<UserProfile>> GetAllUserProfiles(int page, int pageSize, string? searchString)
    {
        return _userProfileRepository.GetUserProfiles(page, pageSize, searchString);
    }

    public Task<List<UserProfile>> GetAllUserProfilesByTag(string tag)
    {
        throw new NotImplementedException();
    }

    public Task<List<UserProfile>> GetAllUserProfilesByUser(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> CreateUserProfile(UserProfile userProfile)
    {
        int currentUserCount = await _userProfileRepository.GetUserProfilesCount();
        var idPart = string.Concat(userProfile.Id.ToString().Take(5));
        userProfile.UserTag = $"user_{currentUserCount}{idPart}";
        if (userProfile.DisplayName == null)
        {
            userProfile.DisplayName = userProfile.UserTag;
        }
        
        
        return await _userProfileRepository.CreateUserProfile(userProfile);
    }

    public Task UpdateUserProfile(UserProfile userProfile)
    {
        return _userProfileRepository.UpdateUserProfile(userProfile);
    }

    public Task DeleteUserProfile(UserProfile userProfile)
    {
        throw new NotImplementedException();
    }
}