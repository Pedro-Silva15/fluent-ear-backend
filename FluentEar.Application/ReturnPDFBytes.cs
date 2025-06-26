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
        var resolver = new FontStyleResolver();
        var frame = section.AddTextFrame();
        frame.Top = Unit.FromPoint(73.93);
        frame.Left = Unit.FromPoint(40 + leftDistanceIncrement);
        frame.Width = Unit.FromPoint(233);
        frame.Height = Unit.FromPoint(706.78);
        frame.RelativeVertical = RelativeVertical.Page;
        frame.RelativeHorizontal = RelativeHorizontal.Page;

        var songTitle = frame.AddParagraph();
        songTitle.AddFormattedText(request.SongTitle, resolver.GetFontSongTitle(request.SongTitle));

        var artistName = frame.AddParagraph();
        artistName.AddFormattedText(request.ArtistName, resolver.GetFontArtistName(request.ArtistName));

        var lyrics = frame.AddParagraph();
        lyrics.AddFormattedText($"\n{request.SongLyrics}", resolver.GetFontSongLyrics(request.SongLyrics));
    }
}