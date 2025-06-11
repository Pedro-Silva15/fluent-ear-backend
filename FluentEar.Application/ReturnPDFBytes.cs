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
        var page = CreatePage(document);

        var paragraph = page.AddParagraph();

        paragraph.AddFormattedText(song.Title, new Font { Name = FontHelper.OPEN_SANS_REGULAR, Size = 20, Bold = true});
        paragraph.AddLineBreak();

        paragraph.AddFormattedText(song.Artist, new Font { Name = FontHelper.OPEN_SANS_REGULAR, Size = 16});
        paragraph.AddLineBreak();

        paragraph.AddFormattedText(song.Lyrics, new Font { Name = FontHelper.OPEN_SANS_REGULAR, Size = 12});

        return RenderDocument(document);
    }

    private Document CreateDocument()
    {
        var document = new Document();
        document.Info.Title = "Fluent Ear";
        document.Info.Subject = "The best project!";

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.OPEN_SANS_REGULAR;

        return document;
    }

    private Section CreatePage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();
        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;

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
