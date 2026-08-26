using SocialService.Domain.Entities;
using SoundAlone.Shared.Contracts.Events;

namespace SocialService.Application.Interfaces;

public interface IUserProfileService
{
    public Task<List<UserProfile>> GetAllUserProfiles(int page, int pageSize, string? searchString);
    
    public Task<UserProfile?> GetUserProfileById(Guid id);
    
    public Task<Guid> CreateUserProfile(UserProfile userProfile);
    
    public Task UpdateUserProfile(UserProfile userProfile);
    
    public Task DeleteUserProfile(UserProfile userProfile);
    
    public Task<UserProfile> GetMe(Guid id);
    
    public Task CreateFromUserRegisteredAsync(UserRegisteredEvent @event, CancellationToken cancellationToken = default);
}