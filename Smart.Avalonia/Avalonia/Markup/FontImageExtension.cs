namespace Smart.Avalonia.Markup;

using System.Globalization;

using global::Avalonia;
using global::Avalonia.Markup.Xaml;
using global::Avalonia.Media;

public sealed class FontImageExtension : MarkupExtension
{
    public string Glyph { get; set; } = string.Empty;

    public FontFamily FontFamily { get; set; } = FontFamily.Default;

    public double Size { get; set; } = 16;

    public IBrush Brush { get; set; } = Brushes.Black;

    public FontWeight FontWeight { get; set; } = FontWeight.Normal;

    public FontStyle FontStyle { get; set; } = FontStyle.Normal;

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        var text = new FormattedText(Glyph, CultureInfo.InvariantCulture, FlowDirection.LeftToRight, new Typeface(FontFamily, FontStyle, FontWeight), Size, Brush);
        return new DrawingImage(new DrawingGroup
        {
            Children =
            {
                new GeometryDrawing { Brush = Brushes.Transparent, Geometry = new RectangleGeometry(new Rect(0, 0, text.Width, text.Height)) },
                new GeometryDrawing { Brush = Brush, Geometry = text.BuildGeometry(default) }
            }
        });
    }
}
