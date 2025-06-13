using FluentEar.Application.Models.Lyrics.Requests.GetLyrics;
using FluentEar.Domain.Entities;

namespace FluentEar.Application.Interfaces;

public interface ILyricsService
{
    Task<SongEntity> GetLyrics(GetLyricsRequest request);
}