using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaylistService.Domain.Entities;

/// <summary>
/// Атрибуты для музыки.
/// Отображают дополнительную информацию о музыке
/// </summary>
public class SongAttributes
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Идентификатор песни
    /// </summary>
    [Required]
    [Column("song_id")]
    public Guid SongId { get; set; }
    
    /// <summary>
    /// Оценка валентности песни
    /// Какой смысл отображает: положительный или отрицательный
    /// </summary>
    [Column("valence")]
    public float Valence { get; set; }
    
    /// <summary>
    /// Оценка волнения песни
    /// Выше 0.5 отображает насколько волнительна песня, ниже спокойна.
    /// Может задаваться автоматически от темпа
    /// </summary>
    [Column("excitement")]
    public float Excitement { get; set; }
    
    /// <summary>
    /// Оценка меланхоличности трека
    /// </summary>
    [Column("melancholy")]
    public float Melancholy { get; set; }
    
    /// <summary>
    /// Оценка агрессивности трека
    /// </summary>
    [Column("aggressiveness")]
    public float Aggressiveness { get; set; }
    
    /// <summary>
    /// Технологичность трека.
    /// Устанавливается в зависимости от технологий и техник используемых в треке
    /// Например если настроение трека ближе к традиционным песням вроде кантри или к классике то значение стремится к 0, и наоборот
    /// если в песне используются синтезаторы и более современные мотивы, значение стремится к 1
    /// </summary>
    [Column("manufacturability")]
    public float Manufacturability { get; set; }
    
    /// <summary>
    /// Темп BPM музыки
    /// </summary>
    [Column("tempo")]
    public float Tempo { get; set; }
    
    /// <summary>
    /// Максимальная громкость трека
    /// </summary>
    [Column("max_volume")]
    public float MaxVolume { get; set; }
    /// <summary>
    /// Средняя громкость трека
    /// </summary>
    [Column("avg_volume")]
    public float AverageVolume { get; set; }
    
    /// <summary>
    /// Значение цвета hue (HSV) основанный на эмоциональности трека
    /// Hue отображает семейство эмоции трека, к семействам могут относиться к разным эмоциям
    /// </summary>
    [Column("sng_clr_h")]
    public float SongColorHue { get; set; }
    /// <summary>
    /// Значение цвета saturation (HSV) основанный на эмоциональности трека
    /// Saturation отображает конкретность эмоции если однозначно нельзя указать одну эмоцию то цвет будет ближе к серому
    /// </summary>
    [Column("sng_clr_s")]
    public float SongColorSaturation { get; set; }
    /// <summary>
    /// Значение цвета value (HSV) основанный на эмоциональности трека
    /// Value отображает интенсивность настроения
    /// </summary>
    [Column("sng_clr_v")]
    public float SongColorValue { get; set; }
    
    /// <summary>
    /// Вывод по эмоции трека основанный на значениях
    /// </summary>
    [Column("song_emotion_verdict")]
    public string? EmotionVerdict { get; set; }
    
    /// <summary>
    /// Отображает насколько система уверена в оценке эмоции
    /// </summary>
    [Column("verdict_acr")]
    public float VerdictAccuracy { get; set; }
    /// <summary>
    /// Жанр
    /// Автоматически подбирается для трека на основе данных громкости
    /// После публикации при не соответствии устанавливается администратором.
    /// Сообщество может голосовать за жанр трека
    /// </summary>
    [Column("song_genre")]
    public string? Genre { get; set; }
}