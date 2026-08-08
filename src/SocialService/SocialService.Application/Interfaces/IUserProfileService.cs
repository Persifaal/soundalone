using SocialService.Domain.Entities;

namespace SocialService.Application.Interfaces;

public interface IUserProfileService
{
    public Task<List<UserProfile>> GetAllUserProfiles();
    
    public Task<List<UserProfile>> GetAllUserProfilesByTag(string tag);
    
    public Task<List<UserProfile>> GetAllUserProfilesByUser(string userId);
    
    public Task<Guid> CreateUserProfile(UserProfile userProfile);
    
    public Task UpdateUserProfile(UserProfile userProfile);
    
    public Task DeleteUserProfile(UserProfile userProfile);
    
    
}