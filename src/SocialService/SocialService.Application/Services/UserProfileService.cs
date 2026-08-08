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
    
    public Task<List<UserProfile>> GetAllUserProfiles()
    {
        return _userProfileRepository.GetUserProfiles();
    }

    public Task<List<UserProfile>> GetAllUserProfilesByTag(string tag)
    {
        throw new NotImplementedException();
    }

    public Task<List<UserProfile>> GetAllUserProfilesByUser(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> CreateUserProfile(UserProfile userProfile)
    {
        userProfile.UserTag = "user_" 
                              + _userProfileRepository.GetUserProfilesCount()
                              + userProfile.Id.ToString().Take(5);
        return _userProfileRepository.CreateUserProfile(userProfile);
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