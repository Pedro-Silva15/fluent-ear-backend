using System.Text.Json.Serialization;

namespace EarFluent.Application.DTOs.Response;

public class SongLyricsResponse
{
    [JsonPropertyName("lyrics")]
    public string SongLyrics { get; set; } = string.Empty;
}