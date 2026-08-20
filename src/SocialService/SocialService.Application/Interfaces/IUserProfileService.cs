using SocialService.Domain.Entities;

namespace SocialService.Application.Interfaces;

public interface IUserProfileService
{
    public Task<List<UserProfile>> GetAllUserProfiles(int page, int pageSize, string? searchString);
    
    public Task<UserProfile?> GetUserProfileById(Guid id);
    
    public Task<Guid> CreateUserProfile(UserProfile userProfile);
    
    public Task UpdateUserProfile(UserProfile userProfile);
    
    public Task DeleteUserProfile(UserProfile userProfile);
    
    
}