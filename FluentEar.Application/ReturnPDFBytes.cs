using FluentEar.Application.Models;
using FluentEar.Application.Models.Lyrics.Requests.GeneratePDF;
using FluentEar.Application.Report.Fonts;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.Rendering;
using PdfSharp.Drawing;
using PdfSharp.Fonts;

namespace FluentEar.Application;

public class ReturnPDFBytes
{
    public ReturnPDFBytes()
    {
        GlobalFontSettings.FontResolver = new SongLyricsReportResolver();
    }

    public byte[] Execute(GeneratePDFRequest request)
    {
        var document = CreateDocument();
        var section = CreatePage(document);

        AddInformations(section, request);

        return RenderDocument(document);
    }

    private static Document CreateDocument()
    {
        var document = new Document();
        document.Info.Title = "Fluent Ear";

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.ANONYMOUS_PRO_REGULAR;

        return document;
    }

    private static Section CreatePage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();
        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.LeftMargin = 0;
        section.PageSetup.RightMargin = 0;
        section.PageSetup.TopMargin = 0;
        section.PageSetup.BottomMargin = 0;

        return section;
    }

    private static byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document
        };

        renderer.RenderDocument();

        DrawRectangle(renderer);

        using var file = new MemoryStream();
        renderer.PdfDocument.Save(file);

        return file.ToArray();
    }

    private static void DrawRectangle(PdfDocumentRenderer renderer)
    {
        var page = renderer.PdfDocument.Pages[0];
        using var graphic = XGraphics.FromPdfPage(page);

        var pen = new XPen(XColor.FromArgb(0xD9, 0xD9, 0xD9), 1);

        double x = 297;
        double y = 30;
        double width = 1;
        double height = 782;

        graphic.DrawRectangle(pen, x, y, width, height);
    }

    private static void AddInformations(Section section, GeneratePDFRequest request)
    {
        double distanceBetweenInformations = 0;
        for(int i = 0 ; i < 2; i++)
        {
            if(request.PrintHeaderAndFooter)
                PrintHeaderAndFooter(section, distanceBetweenInformations);
            AddSongInformations(section, request, distanceBetweenInformations);
            distanceBetweenInformations += 298;
        }
    }

    private static void PrintHeaderAndFooter(Section section, double leftDistanceIncrement)
    {
        var images = LogoImage.GetImages();
        foreach (var imagem in images)
        {
            var frame = section.AddTextFrame();
            frame.Top = Unit.FromPoint(imagem.TopDistanceInPoints);
            frame.Left = Unit.FromPoint(imagem.LeftDistanceInPoints + leftDistanceIncrement);
            frame.RelativeVertical = RelativeVertical.Page;
            frame.RelativeHorizontal = RelativeHorizontal.Page;

            var image = frame.AddImage(imagem.Path);
            image.Width = Unit.FromPoint(imagem.Width);
            image.LockAspectRatio = true;
        }
    }

    private static void AddSongInformations(Section section, GeneratePDFRequest request, double leftDistanceIncrement)
    {
        double maxFont = 14;
        var frame = section.AddTextFrame();
        frame.Top = Unit.FromPoint(73.93);
        frame.Left = Unit.FromPoint(40 + leftDistanceIncrement);
        frame.Width = Unit.FromPoint(233);
        frame.Height = Unit.FromPoint(706.78);
        frame.RelativeVertical = RelativeVertical.Page;
        frame.RelativeHorizontal = RelativeHorizontal.Page;

        CalculateMaxFont(request.SongTitle, FontStyle.SONG_TITLE, ref maxFont);
        var songTitle = frame.AddParagraph();
        songTitle.AddFormattedText(request.SongTitle, new Font() { Name = FontStyle.SONG_TITLE.Name, Size = maxFont });

        var artistName = frame.AddParagraph();
        CalculateMaxFont(request.ArtistName, FontStyle.ARTIST_NAME, ref maxFont);
        artistName.AddFormattedText(request.ArtistName, new Font() { Name = FontStyle.ARTIST_NAME.Name, Size = maxFont });

        var lyrics = frame.AddParagraph();
        CalculateMaxFont(request.SongLyrics, FontStyle.LYRICS, ref maxFont);
        lyrics.AddFormattedText("\n" + request.SongLyrics, new Font() { Name = FontStyle.LYRICS.Name, Size = maxFont });
    }

    private static void CalculateMaxFont(string text, Font font, ref double maxFontSize)
    {
        const double MAX_WIDTH = 233;
        const double MAX_HEIGHT = 706.78;

        var verses = text.Split('\n');
        int versesCount = verses.Length;

        string longestVerse = verses
            .OrderByDescending(verse => verse.Length)
            .First();

        var xFont = new XFont(font.Name, font.Size.Point);

        using var graphics = XGraphics.CreateMeasureContext(new XSize(MAX_WIDTH, MAX_HEIGHT), XGraphicsUnit.Point, XPageDirection.Downwards);

        var size = graphics.MeasureString(longestVerse, xFont);

        double widthInPoints = size.Width;
        double heighInPoints = size.Height * versesCount;

        maxFontSize = font.Size.Point;
        if(MAX_WIDTH < widthInPoints)
            maxFontSize = (MAX_WIDTH * font.Size.Point) / widthInPoints;
        
        if(MAX_HEIGHT < heighInPoints)
            maxFontSize = (MAX_HEIGHT * maxFontSize) / heighInPoints;
    }
}