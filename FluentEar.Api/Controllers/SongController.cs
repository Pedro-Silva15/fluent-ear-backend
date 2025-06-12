using FluentEar.Application.Interfaces;
using FluentEar.Application.Models.Lyrics.Requests;
using Microsoft.AspNetCore.Mvc;

namespace FluentEar.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SongController(ISongService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetLyrics([FromQuery]GetLyricsRequest request)
    {
        var validator = new GetLyricsRequestValidator();
        var result = validator.Validate(request);

        if(!result.IsValid)
            return BadRequest(result.Errors.Select(error => error.ErrorMessage).ToList());

        var song = await service.GetLyrics(request);
        return Ok(song);
    }
}