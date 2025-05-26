namespace EarFluent.Domain;

public class Song
{
    public string Artist { get; set; }
    public string Name { get; set; }
    public string? Lyrics { get; set; } = string.Empty;

    public Song(string artist, string name, string? lyrics)
    {
        if (string.IsNullOrEmpty(artist) || string.IsNullOrEmpty(name))
            throw new ArgumentException("Parâmetros inválidos!");

        Artist = artist;
        Name = name;
        Lyrics = lyrics;
    }
}