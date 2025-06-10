using System.Text.Json.Serialization;

namespace FluentEar.Application.DTOs.Response;

public class SongLyricsResponse
{
    [JsonPropertyName("lyrics")]
    public string SongLyrics { get; set; } = string.Empty;
}