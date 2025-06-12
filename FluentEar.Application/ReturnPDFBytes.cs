using FluentEar.Application.Report.Fonts;
using FluentEar.Domain.Entities;
using MigraDoc.DocumentObjectModel;
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

    public byte[] Execute(SongEntity song)
    {
        var document = CreateDocument();
        var section = CreatePage(document);

        var image = section.AddImage(@"C:\Users\po4747\Desktop\GitHub\fluent-ear-backend\FluentEar.Application\Assets\FluentEarLogo.png");
        image.Width = Unit.FromPoint(99.59);
        image.LockAspectRatio = true;
        image.Top = Unit.FromPoint(35);
        image.Left = Unit.FromPoint(178);

        var songTitle = section.AddParagraph();
        songTitle.Format.LeftIndent = Unit.FromPoint(40);
        songTitle.Format.SpaceBefore = Unit.FromPoint(14.21);
        songTitle.AddFormattedText(song.Title, new Font { Name = FontHelper.OPEN_SANS_SEMIBOLD, Size = 14 });

        var artist = section.AddParagraph();
        artist.AddFormattedText(song.Artist, new Font { Name = FontHelper.OPEN_SANS_SEMIBOLD, Size = 10 });

        var lyrics = section.AddParagraph();
        lyrics.Format.LeftIndent = Unit.FromPoint(40);
        lyrics.Format.SpaceBefore = Unit.FromPoint(6.94);
        lyrics.AddFormattedText(song.Lyrics, new Font { Name = FontHelper.OPEN_SANS_REGULAR, Size = 8 });

        //var footer = section.Footers.Primary.AddParagraph();
        //footer.Format.LeftIndent = Unit.FromPoint(127.37);
        //footer.Format.SpaceBefore = Unit.FromPoint(6.94);
        //footer.Format.Font.Name = FontHelper.OPEN_SANS_REGULAR;
        //footer.Format.Font.Size = 6;
        //footer.AddFormattedText("Developed by: Pedro Silva", new Font { Name = FontHelper.OPEN_SANS_LIGHTITALIC, Size = 5 });

        return RenderDocument(document);
    }

    private static Document CreateDocument()
    {
        var document = new Document();
        document.Info.Title = "Fluent Ear";

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.OPEN_SANS_REGULAR;

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

    private static void AddImage(string fileName, )
    {

    }

}
