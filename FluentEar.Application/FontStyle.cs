using FluentEar.Application.Report.Fonts;
using MigraDoc.DocumentObjectModel;

namespace FluentEar.Application;

public static class FontStyle
{
    public static readonly Font SONG_TITLE = new() { Name = FontHelper.ANONYMOUS_PRO_BOLD, Size = Unit.FromPoint(14) };
    public static readonly Font ARTIST_NAME = new() { Name = FontHelper.ANONYMOUS_PRO_BOLD, Size = Unit.FromPoint(10) };
    public static readonly Font LYRICS = new() { Name = FontHelper.ANONYMOUS_PRO_REGULAR, Size = Unit.FromPoint(8.5) };
}