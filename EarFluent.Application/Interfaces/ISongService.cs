using EarFluent.Application.DTOs.Requests;
using EarFluent.Domain.Entities;

namespace EarFluent.Application.Interfaces;

public interface ISongService
{
    Task<SongEntity> GetLyrics(SongLyricsRequest request);
}
