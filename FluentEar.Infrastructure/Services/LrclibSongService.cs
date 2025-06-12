using FluentEar.Application.DTOs.Response;
using FluentEar.Application.Interfaces;
using FluentEar.Application.Models.Lyrics.Requests;
using FluentEar.Domain.Entities;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using FluentEar.Infrastructure.Adapters;

namespace FluentEar.Infrastructure.Services;

public class LrclibSongService : ISongService
{
    private readonly HttpClient _client;
    private readonly string _apiUrl = "https://lrclib.net/api/search?";

    public LrclibSongService(HttpClient client)
    {
        _client = client;
    }

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
