namespace Smart.Avalonia.Data;

using System.Globalization;

using global::Avalonia;
using global::Avalonia.Data.Converters;

public sealed class ToUpperConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is string text ? text.ToUpper(culture) : AvaloniaProperty.UnsetValue;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return AvaloniaProperty.UnsetValue;
    }
}
