using FluentEar.Application;
using FluentEar.Application.Models.Lyrics.Requests;
using FluentEar.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace FluentEar.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SongPDFController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPdf([FromQuery] GetLyricsRequest request)
    {
        var client = new HttpClient();
        var response = await client.GetAsync($"https://localhost:7056/api/song?artist={request.Artist}&songtitle={request.SongTitle}");
        var songEntity = await response.Content.ReadFromJsonAsync<SongEntity>();

        byte[] file = new ReturnPDFBytes().Execute(songEntity!);

        if (file.Length > 0)
            return File(file, MediaTypeNames.Application.Pdf, $"{request.Artist} - {request.SongTitle}.pdf");

        return Ok();
    }
}
