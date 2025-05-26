using Microsoft.AspNetCore.Mvc;
using EarFluent.Requests;
using EarFluent.Application.Interfaces;
using System.Text.Json;

namespace EarFluent.Ui.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SongController : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetLyrics([FromQuery] LyricsRequest request, [FromServices] ISongService service)
    {
        var song = await service.GetLyrics(request);
        if (song == null)
            return BadRequest("Algo deu errado!");
        return Ok(JsonSerializer.Serialize(song));
    }
}