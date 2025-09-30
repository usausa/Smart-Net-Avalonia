namespace Smart.Avalonia.Data;

using System.Globalization;

using global::Avalonia.Data.Converters;
using global::Avalonia.Media;

public sealed class ColorToBrushConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is Color color ? new SolidColorBrush(color) : null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is SolidColorBrush brush ? brush.Color : null;
    }
}
