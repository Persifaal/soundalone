using SocialService.Domain.Entities;

namespace SocialService.Application.Interfaces;

public interface IUserProfileRepository
{
    /// <summary>
    /// Получить профиль пользователя по айди
    /// </summary>
    /// <param name="userId">ID</param>
    /// <returns>Пользователь</returns>
    public Task<UserProfile> GetUserProfileById(Guid userId);
    
    /// <summary>
    /// Получить профиль пользователя по тегу
    /// </summary>
    /// <param name="tag">Tag</param>
    /// <returns>Пользователь</returns>
    public Task<UserProfile> GetUserProfileByTag(string tag);
    
    /// <summary>
    /// Получить список пользователей со схожим тегом
    /// </summary>
    /// <param name="tag">Тег</param>
    /// <returns>Пользователи</returns>
    public Task<List<UserProfile>> GetUsersProfileByTag(string tag);
    
    /// <summary>
    /// Получить список профилей пользователей
    /// </summary>
    /// <returns>Список пользователей</returns>
    public Task<List<UserProfile>> GetUserProfiles();
    
    /// <summary>
    /// Создать профиля пользователя
    /// </summary>
    /// <param name="userProfile">Данные пользователя</param>
    public Task<Guid> CreateUserProfile(UserProfile userProfile);
    
    /// <summary>
    /// Обновить данные профиля пользователя
    /// </summary>
    /// <param name="userProfile">Данные пользователя</param>
    public Task UpdateUserProfile(UserProfile userProfile);
    
    /// <summary>
    /// Получить число пользователей с профилями в системе
    /// </summary>
    /// <returns></returns>
    public Task<int> GetUserProfilesCount();
}