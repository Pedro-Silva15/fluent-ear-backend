namespace FluentEar.Application.Models;

internal class LogoImage
{
    public string Path { get; set; } = string.Empty;
    public double Width { get; set; }
    public double TopDistanceInPoints { get; set; }
    public double LeftDistanceInPoints { get; set; }

    public static List<LogoImage> GetImages()
    {
        var imagesList = new List<LogoImage>
        {
            new()
            {
                Path = @"C:\Users\pedro\Desktop\GitHub\FluentEar\Backend\FluentEar.Application\Assets\FluentEarLogo.png",
                Width = 99.59,
                TopDistanceInPoints = 35,
                LeftDistanceInPoints = 178
            },
            new()
            {
                Path = @"C:\Users\pedro\Desktop\GitHub\FluentEar\Backend\FluentEar.Application\Assets\Footer.png",
                Width = 62.25,
                TopDistanceInPoints = 794,
                LeftDistanceInPoints = 127.37
            }
        };
        return imagesList;
    }
}