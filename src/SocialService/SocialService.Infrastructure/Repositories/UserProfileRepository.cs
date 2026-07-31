using Microsoft.EntityFrameworkCore;
using SocialService.Application.Interfaces;
using SocialService.Domain.Entities;
using SocialService.Infrastructure.Persistence;

namespace SocialService.Infrastructure.Repositories;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly SocialDbContext _context;

    public UserProfileRepository(SocialDbContext context)
    {
        _context = context;
    }
    
    public async Task<UserProfile> GetUserProfileById(Guid userId)
    {
        return await _context.UserProfiles.FirstAsync(x => x.Id == userId);
    }

    public async Task<UserProfile> GetUserProfileByTag(string tag)
    {
        return await _context.UserProfiles.FirstAsync(x => x.UserTag == tag);
    }

    public async Task<List<UserProfile>> GetUsersProfileByTag(string tag)
    {
        return await _context.UserProfiles.Where(x => x.UserTag.StartsWith(tag)).ToListAsync();
    }
    
    public async Task<List<UserProfile>> GetUserProfiles()
    {
        return await _context.UserProfiles.ToListAsync();
    }

    public async Task CreateUserProfile(UserProfile userProfile)
    {
        if(userProfile == null)
            throw new ArgumentNullException(nameof(userProfile));
        
        await _context.AddAsync(userProfile);
    }

    public async Task UpdateUserProfile(UserProfile userProfile)
    {
        if (userProfile == null || userProfile.Id == null)
            throw new ArgumentNullException(nameof(userProfile));
        
        var userToChange = await _context.UserProfiles.FirstAsync(x => x.Id == userProfile.Id);
        
        userToChange.UserTag = userProfile.DisplayName ?? userToChange.DisplayName;
        userToChange.Bio =  userProfile.Bio ?? userToChange.Bio;
        
        if(_context.UserProfiles.Any(x => x.UserTag == userProfile.UserTag && x.Id != userProfile.Id))
            throw new InvalidOperationException("User with same tag already exists");
        
        userToChange.UserTag = userProfile.UserTag ?? userToChange.UserTag;
        userToChange.AvatarUrl = userProfile.AvatarUrl ?? userToChange.AvatarUrl;
        userToChange.BannerUrl = userProfile.BannerUrl ?? userToChange.BannerUrl;
        
        _context.UserProfiles.Update(userToChange);
        await _context.SaveChangesAsync();
    }
}