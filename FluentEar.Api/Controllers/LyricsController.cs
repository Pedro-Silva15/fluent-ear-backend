using FluentEar.Application;
using FluentEar.Application.Interfaces;
using FluentEar.Application.Models.Lyrics.Requests.GeneratePDF;
using FluentEar.Application.Models.Lyrics.Requests.GetLyrics;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace FluentEar.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LyricsController(ILyricsService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetLyrics([FromQuery] GetLyricsRequest request)
    {
        var validator = new GetLyricsRequestValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
            return BadRequest(result.Errors.Select(error => error.ErrorMessage).ToList());

        var song = await service.GetLyrics(request);
        return Ok(song);
    }

    [HttpPost]
    public IActionResult GeneratePDF(GeneratePDFRequest request)
    {
        var validator = new GeneratePdfRequestValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
            return BadRequest(result.Errors.Select(error => error.ErrorMessage).ToList());

        byte[] file = new ReturnPDFBytes().Execute(request);

        if (0 < file.Length)
            return File(file, MediaTypeNames.Application.Pdf, $"{request.ArtistName} - {request.SongTitle}.pdf");

        return NoContent();
    }

    [HttpGet("letter")]
    public IActionResult SeeLetterWidth(string letter)
    {

        return NoContent();
    }
}