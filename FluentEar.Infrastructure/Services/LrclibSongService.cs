using FluentEar.Application.Interfaces;
using FluentEar.Application.Models.Lyrics.Requests.GetLyrics;
using FluentEar.Domain.Entities;
using FluentEar.Infrastructure.Adapters;
using System.Net.Http.Json;

namespace FluentEar.Infrastructure.Services;

public class LrclibSongService(HttpClient client) : ILyricsService
{
    private readonly HttpClient _client = client;
    private readonly string _apiUrl = "https://lrclib.net/api/search?";

    public async Task<SongEntity> GetLyrics(GetLyricsRequest request)
    {
        var response = await _client.GetAsync($"{_apiUrl}artist_name={request.Artist}&track_name={request.SongTitle}");
        if (!response.IsSuccessStatusCode)
            throw new Exception("The lyrics song was not found!");
        var songsList = await response.Content.ReadFromJsonAsync<List<LrclibAdapter>>();
        string songLyrics = songsList![0].SongLyrics;
        return new SongEntity(request.Artist, request.SongTitle, songLyrics);
    }
}
