using FluentValidation;

namespace FluentEar.Application.Models.Lyrics.Requests.GeneratePDF;

public class GeneratePDFRequestValidator :  AbstractValidator<GeneratePDFRequest>
{
    public GeneratePDFRequestValidator()
    {
        RuleFor(request => request.ArtistName).NotEmpty().WithMessage("Artist name cannot empty.");
        RuleFor(request => request.SongTitle).NotEmpty().WithMessage("Song title cannot be empty.");
        RuleFor(request => request.Lyrics).NotEmpty().WithMessage("Lyrics name cannot empty.");
    }
}
