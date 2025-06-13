namespace FluentEar.Application.Models.Lyrics.Requests.GetLyrics;

public class GetLyricsRequest
{
    public string SongTitle { get; set; } = string.Empty;
    public string Artist { get; set; } = string.Empty;
}