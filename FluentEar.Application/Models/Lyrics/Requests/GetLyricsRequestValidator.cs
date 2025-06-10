using FluentValidation;

namespace FluentEar.Application.Models.Lyrics.Requests;
public class GetLyricsRequestValidator : AbstractValidator<GetLyricsRequest>
{
    public GetLyricsRequestValidator()
    {
        RuleFor(request => request.Artist).NotEmpty().WithMessage("Artist name cannot be null or empty");
        RuleFor(request => request.SongTitle).NotEmpty().WithMessage("Song title cannot be null or empty");
    }
}
