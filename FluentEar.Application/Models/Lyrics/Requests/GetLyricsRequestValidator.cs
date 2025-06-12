using FluentValidation;

namespace FluentEar.Application.Models.Lyrics.Requests;

public class GetLyricsRequestValidator : AbstractValidator<GetLyricsRequest>
{
    public GetLyricsRequestValidator()
    {
        RuleFor(request => request.SongTitle).NotEmpty().WithMessage("Song title cannot be empty.");
        RuleFor(request => request.Artist).NotEmpty().WithMessage("Artist name cannot empty.");
    }
}