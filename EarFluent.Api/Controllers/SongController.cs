using Microsoft.AspNetCore.Mvc;
using EarFluent.Application.Interfaces;
using System.Text.Json;
using EarFluent.Application.DTOs.Requests;

namespace EarFluent.Ui.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SongController : ControllerBase
{
    private readonly ISongService _songService;
    public SongController(ISongService service)
    {
        _songService = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetLyrics([FromQuery] SongLyricsRequest request)
    {
        var song = await _songService.GetLyrics(request);
        if (song == null)
            return BadRequest("Algo deu errado!");
        return Ok(JsonSerializer.Serialize(song));
    }
}