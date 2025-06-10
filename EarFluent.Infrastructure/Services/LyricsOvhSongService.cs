using EarFluent.Application.DTOs.Response;
using EarFluent.Application.Interfaces;
using EarFluent.Application.Models.Lyrics.Requests;
using EarFluent.Domain.Entities;
using System.Net.Http.Json;

namespace EarFluent.Infrastructure.Services;

public class LyricsOvhSongService : ISongService
{
    private readonly HttpClient _client;
    private readonly string _apiUrl = Environment.GetEnvironmentVariable("lyricsUrl") ?? throw new InvalidOperationException("The api url is not set.");

    public LyricsOvhSongService(HttpClient client)
    {
        _client = client;
    }

    public async Task<SongEntity> GetLyrics(GetLyricsRequest request)
    {
        var response = await _client.GetAsync($"{_apiUrl}/{request.Artist}/{request.SongTitle}");
        if (!response.IsSuccessStatusCode)
            throw new Exception("The lyrics song was not found!");
        var lyricsResponse = await response.Content.ReadFromJsonAsync<SongLyricsResponse>();
        return new SongEntity(request.Artist, request.SongTitle, lyricsResponse?.SongLyrics!);
    }
}
