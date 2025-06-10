namespace FluentEar.Application.Models.Lyrics.Requests;

public class GetLyricsRequest
{
    public string Artist { get; set; } = string.Empty;
    public string SongTitle { get; set; } = string.Empty;
}