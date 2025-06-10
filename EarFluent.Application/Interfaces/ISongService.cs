using EarFluent.Application.Models.Lyrics.Requests;
using EarFluent.Domain.Entities;

namespace EarFluent.Application.Interfaces;

public interface ISongService
{
    Task<SongEntity> GetLyrics(GetLyricsRequest request);
}
