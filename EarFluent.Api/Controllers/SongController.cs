using Microsoft.AspNetCore.Mvc;
using EarFluent.Application.Interfaces;
using System.Text.Json;
using EarFluent.Application.Models.Lyrics.Requests;

namespace EarFluent.Api.Controllers;

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
    public async Task<IActionResult> GetLyrics([FromQuery] GetLyricsRequest request)
    {
        var validator = new GetLyricsRequestValidator();
        var result = validator.Validate(request);

        if(!result.IsValid)
            return BadRequest(result.Errors.Select(error => error.ErrorMessage).ToList());

        var song = await _songService.GetLyrics(request);
        return Ok(song);
    }
}