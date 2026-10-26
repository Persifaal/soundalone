using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlaylistService.Application.Interfaces.Services;
using PlaylistService.Domain.Entities;

namespace PlaylistService.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PlaylistController : ControllerBase
{
    private readonly IPlaylistService _playlistsService;
    IWebHostEnvironment _appEnvironment;

    public PlaylistController(IPlaylistService playlistsService, IWebHostEnvironment appEnvironment)
    {
        _playlistsService = playlistsService;
        _appEnvironment = appEnvironment;
    }

    [HttpGet("/playlists-all/")]
    public async Task<List<Playlist>> GetAllAsync()
    {
        List<Playlist> playlists = await _playlistsService.GetAllAsync();

        return playlists;
    }


    [HttpGet("/playlists")]
    public async Task<Playlist?> GetByIdAsync([FromQuery] Guid id)
    {
        Playlist? playlist = await _playlistsService.GetAsync(id);

        return playlist;
    }

    [HttpPost("/playlists")]
    public async Task<Guid> CreatePlaylist([FromBody] Playlist playlist)
    {
        var id = await _playlistsService.CreatePlaylist(playlist);

        return id;
    }
    
    [Authorize]
    [HttpPost("/playlist-cover")]
    public ActionResult UpdateCover([FromQuery] Guid id, IFormFile cover)
    {
        if (cover.Length > 20 * 1024 * 1024)
            return BadRequest("File cannot contain more than 20 MB");

        using(var fileStream = new FileStream(_appEnvironment.WebRootPath + id, FileMode.Create))
        {
            _playlistsService.UploadCover(id, cover.FileName, fileStream);
        }
        return Ok();
    }

}