using Microsoft.Extensions.Logging;
using SocialService.Application.Interfaces;
using SocialService.Domain.Entities;
using SoundAlone.Shared.Contracts.Events;

namespace SocialService.Application.Services;

public class UserProfileService : IUserProfileService
{
    private IUserProfileRepository _userProfileRepository;
    private readonly ILogger<UserProfileService> _logger;

    public UserProfileService(IUserProfileRepository repository, ILogger<UserProfileService> logger)
    {
        _userProfileRepository = repository;
        _logger = logger;
    }
    
    public Task<List<UserProfile>> GetAllUserProfiles(int page, int pageSize, string? searchString)
    {
        return _userProfileRepository.GetUserProfiles(page, pageSize, searchString);
    }

    public Task<UserProfile?> GetUserProfileById(Guid id)
    {
        return _userProfileRepository.GetUserProfileById(id);
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

    public async Task<UserProfile> GetMe(Guid id)
    {
        return await _userProfileRepository.GetUserByAccountId(id);
    }

    public async Task CreateFromUserRegisteredAsync(
        UserRegisteredEvent @event,
        CancellationToken cancellationToken = default)
    {
        var profile = new UserProfile
        {
            Id = Guid.NewGuid(),
            RelatedAccount = @event.UserId,
            DisplayName = @event.Username,
            UserTag = @event.Username,
        };

        await _userProfileRepository.CreateUserProfile(profile);

        _logger.LogInformation(
            "Profile created for user {UserId}",
            @event.UserId);
    }
}