using FluentEar.Application.Report.Fonts;
using MigraDoc.DocumentObjectModel;
using PdfSharp.Drawing;

namespace FluentEar.Application;

public class FontStyleResolver
{
    private double MaxFontSize = 14;

    public Font GetFontSongTitle(string text)
    {
        var font = new Font { Name = FontHelper.ANONYMOUS_PRO_BOLD, Size = Unit.FromPoint(MaxFontSize) };
        UpdateMaxFont(text, font);
        return new Font { Name = font.Name, Size = Unit.FromPoint(MaxFontSize) };
    }

    public Font GetFontArtistName(string text)
    {
        var font = new Font { Name = FontHelper.ANONYMOUS_PRO_BOLD, Size = Unit.FromPoint(MaxFontSize / 1.4) };
        UpdateMaxFont(text, font);
        return new Font { Name = font.Name, Size = Unit.FromPoint(MaxFontSize) };
    }

    public Font GetFontSongLyrics(string text)
    {
        var font = new Font { Name = FontHelper.ANONYMOUS_PRO_REGULAR, Size = Unit.FromPoint(MaxFontSize / 1.25) };
        UpdateMaxFont(text, font);
        return new Font { Name = font.Name, Size = Unit.FromPoint(MaxFontSize) };
    }

    private void UpdateMaxFont(string text, Font font)
    {
        const double MAX_WIDTH = 233;
        const double MAX_HEIGHT = 706.78;

        var verses = text.Split('\n');
        int versesCount = verses.Length;

        string longestVerse = verses.OrderByDescending(v => v.Length).FirstOrDefault() ?? "";

        var xFont = new XFont(font.Name, font.Size.Point);

        using var graphics = XGraphics.CreateMeasureContext(new XSize(MAX_WIDTH, MAX_HEIGHT), XGraphicsUnit.Point, XPageDirection.Downwards);

        var size = graphics.MeasureString(longestVerse, xFont);

        double widthInPoints = size.Width;
        double heightInPoints = size.Height * versesCount;

        double candidateFontSize = font.Size.Point;

        if (MAX_WIDTH < widthInPoints)
            candidateFontSize = (MAX_WIDTH * candidateFontSize) / widthInPoints;

        if (MAX_HEIGHT < heightInPoints)
            candidateFontSize = (MAX_HEIGHT * candidateFontSize) / heightInPoints;

        MaxFontSize = candidateFontSize;
    }
}
