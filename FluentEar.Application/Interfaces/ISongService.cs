using FluentEar.Application.Models.Lyrics.Requests;
using FluentEar.Domain.Entities;

namespace FluentEar.Application.Interfaces;

public interface ISongService
{
    Task<SongEntity> GetLyrics(GetLyricsRequest request);
}
