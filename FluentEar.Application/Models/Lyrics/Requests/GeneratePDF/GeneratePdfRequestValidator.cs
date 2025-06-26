using FluentValidation;

namespace FluentEar.Application.Models.Lyrics.Requests.GeneratePDF;

public class GeneratePdfRequestValidator :  AbstractValidator<GeneratePDFRequest>
{
    public GeneratePdfRequestValidator()
    {
        RuleFor(request => request.ArtistName).NotEmpty().WithMessage("Artist name cannot empty.");
        RuleFor(request => request.SongTitle).NotEmpty().WithMessage("Song title cannot be empty.");
        RuleFor(request => request.SongLyrics).NotEmpty().WithMessage("Lyrics name cannot empty.");
    }
}
