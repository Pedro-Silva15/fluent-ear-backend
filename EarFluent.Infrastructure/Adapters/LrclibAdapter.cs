using System.Text.Json.Serialization;

namespace EarFluent.Infrastructure.Adapters;

internal class LrclibAdapter
{
    [JsonPropertyName("plainLyrics")]
    public string SongLyrics { get; set; } = string.Empty;
}
