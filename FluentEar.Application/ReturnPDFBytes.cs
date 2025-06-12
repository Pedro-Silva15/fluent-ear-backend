using FluentEar.Application.Report.Fonts;
using FluentEar.Domain.Entities;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
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

        var image = section.AddImage(@"C:\Users\pedro\Desktop\Assets\Logo.png");
        image.Width = Unit.FromPoint(99.59);
        image.LockAspectRatio = true;
        image.Top = Unit.FromPoint(35);
        image.Left = Unit.FromPoint(178);

        var paragraph = section.AddParagraph();
        paragraph.Format.LeftIndent = Unit.FromPoint(40);
        paragraph.Format.SpaceBefore = Unit.FromPoint(14.21);
        paragraph.AddFormattedText(song.Title, new Font { Name = FontHelper.OPEN_SANS_SEMIBOLD, Size = 14 });
        paragraph.AddLineBreak();
        paragraph.AddFormattedText(song.Artist, new Font { Name = FontHelper.OPEN_SANS_SEMIBOLD, Size = 10 });

        var lyrics = section.AddParagraph();
        lyrics.Format.LeftIndent = Unit.FromPoint(40);
        lyrics.Format.SpaceBefore = Unit.FromPoint(6.94);
        lyrics.AddFormattedText(song.Lyrics, new Font { Name = FontHelper.OPEN_SANS_REGULAR, Size = 6 });

        // Rodapé posicionado a 10.29pt da borda inferior
        section.PageSetup.FooterDistance = Unit.FromPoint(10.29);

        var footer = section.Footers.Primary.AddParagraph();
        footer.Format.LeftIndent = Unit.FromPoint(127.37);
        footer.Format.SpaceBefore = Unit.FromPoint(6.94);
        footer.Format.Font.Name = FontHelper.OPEN_SANS_REGULAR;
        footer.Format.Font.Size = 6;
        footer.AddFormattedText("Developed by: Pedro Silva", new Font { Name = FontHelper.OPEN_SANS_LIGHTITALIC, Size = 5 });

        return RenderDocument(document);
    }

    private Document CreateDocument()
    {
        var document = new Document();
        document.Info.Title = "Fluent Ear";

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.OPEN_SANS_REGULAR;

        return document;
    }

    private Section CreatePage(Document document)
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

    private byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document
        };

        renderer.RenderDocument();

        using var file = new MemoryStream();
        renderer.PdfDocument.Save(file);

        return file.ToArray();
    }
}
