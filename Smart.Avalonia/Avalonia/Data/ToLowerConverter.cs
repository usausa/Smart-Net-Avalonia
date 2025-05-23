namespace Smart.Avalonia.Data;

using System.Globalization;

using global::Avalonia;
using global::Avalonia.Data.Converters;

public sealed class ToLowerConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is string text ? text.ToLower(culture) : AvaloniaProperty.UnsetValue;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return AvaloniaProperty.UnsetValue;
    }
}
