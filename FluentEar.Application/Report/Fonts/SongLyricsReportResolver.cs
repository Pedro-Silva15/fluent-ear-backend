using PdfSharp.Fonts;
using System.Reflection;

namespace FluentEar.Application.Report.Fonts;

public class SongLyricsReportResolver : IFontResolver
{
    public byte[]? GetFont(string faceName)
    {
        var stream = ReadFontFile(faceName);

        stream ??= ReadFontFile(FontHelper.OPEN_SANS_REGULAR);

        var length = (int)stream!.Length;

        var data = new byte[length];

        stream.ReadExactly(buffer: data, offset: 0, count: length);

        return data;
    }

    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        return new FontResolverInfo(familyName);
    }

    private Stream? ReadFontFile(string faceName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        return assembly.GetManifestResourceStream($"FluentEar.Application.Report.Fonts.{faceName}.ttf");
    }
}
