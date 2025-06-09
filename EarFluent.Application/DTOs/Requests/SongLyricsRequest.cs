namespace EarFluent.Application.DTOs.Requests;

public class SongLyricsRequest
{
    public string Artist { get; set; } = string.Empty;
    public string SongTitle { get; set; } = string.Empty;
}
