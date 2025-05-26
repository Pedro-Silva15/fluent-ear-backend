using EarFluent.Application.DTOs;
using EarFluent.Application.Interfaces;
using EarFluent.Domain;
using EarFluent.Requests;
using System.Net.Http.Json;

namespace EarFluent.Infrastructure.Services;

public class SongService : ISongService
{
    private readonly HttpClient _client;

    public SongService(HttpClient client)
    {
        _client = client;
    }

    public async Task<Song> GetLyrics(LyricsRequest request)
    {
        var response = await _client.GetAsync($"https://api.lyrics.ovh/v1/{request.Artist}/{request.Name}");
        var lyricsResponse = await response.Content.ReadFromJsonAsync<LyricsResponse>();
        return new Song(request.Artist, request.Name, lyricsResponse?.Lyrics);
    }

    public void StandardizeLyrics(string lyrics)
    {
        throw new NotImplementedException();
    }
}
