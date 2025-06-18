namespace FluentEar.Application.Models.Lyrics.Requests.GeneratePDF;

public class GeneratePDFRequest
{
    public string ArtistName { get; set; } = string.Empty;
    public string SongTitle { get; set; } = string.Empty;
    public string SongLyrics { get; set; } = string.Empty;
    public bool PrintHeaderAndFooter { get; set; } = true;
}
