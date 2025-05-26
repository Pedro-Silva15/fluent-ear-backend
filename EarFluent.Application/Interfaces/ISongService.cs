using EarFluent.Domain;
using EarFluent.Requests;

namespace EarFluent.Application.Interfaces;

public interface ISongService
{
    Task<Song> GetLyrics(LyricsRequest request);
    void StandardizeLyrics(string lyrics);
}
