namespace PlaylistService.Domain.Enums;

/// <summary>
/// Доступ к плейлисту
/// </summary>
public enum PlaylistAccess
{
    /// <summary>
    /// Публичный доступ, плейлист видят все и он отображается в сообществе
    /// </summary>
    Public,
    
    /// <summary>
    /// Приватный доступ, плейлист видят только его авторы
    /// </summary>
    Private,
    
    /// <summary>
    /// Доступ только друзьям
    /// </summary>
    OnlyFriends,
    
    /// <summary>
    /// Настраиваемый доступ, автор может указать, кто видит плейлист
    /// </summary>
    Managed
}