using Microsoft.Extensions.Logging;
using PlaylistService.Application.Interfaces.Repositories;
using PlaylistService.Application.Interfaces.Services;
using PlaylistService.Domain.Entities;

namespace PlaylistService.Application.Services;

public class SongsService : ISongsService
{
    private readonly ILogger<SongsService> _logger;
    private  readonly ISongsRepository _songsRepository;
    
    public SongsService(ILogger<SongsService> logger, ISongsRepository songsRepository)
    {
        _logger = logger;
        _songsRepository = songsRepository;
    }

    public Guid AddSong(Song song)
    {
        throw new NotImplementedException();
    }

    public Song GetSong(Guid id)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Вычисляет эмоцию для трека на основе данных
    /// Так же помимо самой эмоции создает соответствующий цвет для визуализации: цвета со средним значением ниже 0,5 отображают негативные эмоции, выше или равные 0,5 позитивные
    /// </summary>
    /// <param name="songAttributes"></param>
    private void CalculateSongEmotion(SongAttributes songAttributes)
    {
        
    }
}