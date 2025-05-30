namespace Smart.Avalonia.Data;

using System.Globalization;

using global::Avalonia;
using global::Avalonia.Data.Converters;
using global::Avalonia.Media;

#pragma warning disable IDE0032
public sealed class ColorBlendConverter : IValueConverter
{
    private double raito;

    public Color Color { get; set; }

    public double Raito
    {
        get => raito;
        set
        {
            if ((value < 0d) || (value > 1d))
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            raito = value;
        }
    }

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Color color)
        {
            return AvaloniaProperty.UnsetValue;
        }

        var r = Math.Min((byte)Math.Round(color.R + ((Color.R - color.R) * raito)), (byte)255);
        var g = Math.Min((byte)Math.Round(color.G + ((Color.G - color.G) * raito)), (byte)255);
        var b = Math.Min((byte)Math.Round(color.B + ((Color.B - color.B) * raito)), (byte)255);
        return Color.FromRgb(r, g, b);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return AvaloniaProperty.UnsetValue;
    }
}
