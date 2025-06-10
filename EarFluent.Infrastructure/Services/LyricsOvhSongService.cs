using EarFluent.Application.DTOs.Response;
using EarFluent.Application.Interfaces;
using EarFluent.Application.Models.Lyrics.Requests;
using EarFluent.Domain.Entities;
using System.Net.Http.Json;

namespace EarFluent.Infrastructure.Services;

public class LyricsOvhSongService : ISongService
{
    private readonly HttpClient _client;
    private readonly string _apiUrl;

    public LyricsOvhSongService(HttpClient client, string apiUrl)
    {
        _client = client;
        _apiUrl = apiUrl;
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
