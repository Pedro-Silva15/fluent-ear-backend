using System.Globalization;

namespace FluentEar.Domain.Entities;

public class SongEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Artist { get; private set; }
    public string Title { get; private set; }
    public string Lyrics { get; private set; }

    public SongEntity(string artist, string title, string lyrics)
    {
        Artist = ConvertToTitleCase(artist);
        Title = ConvertToTitleCase(title);
        Lyrics = lyrics;
    }

    private static string ConvertToTitleCase(string phrase)
    {
        TextInfo textoInfo = CultureInfo.CurrentCulture.TextInfo;
        return textoInfo.ToTitleCase(phrase.ToLower());
    }
}
