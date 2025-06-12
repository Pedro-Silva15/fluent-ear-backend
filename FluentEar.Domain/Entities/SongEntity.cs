namespace FluentEar.Domain.Entities;

public class SongEntity
{
    public Guid Id { get; private set; }
    public string Artist { get; private set; }
    public string Title { get; private set; }
    public string Lyrics { get; private set; }

    public SongEntity(string artist, string title, string lyrics)
    {
        Id = Guid.NewGuid();
        Artist = artist;
        Title = title;
        Lyrics = lyrics;
    }
}