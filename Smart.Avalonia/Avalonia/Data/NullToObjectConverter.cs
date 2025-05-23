namespace Smart.Avalonia.Data;

using System.Globalization;

using global::Avalonia;
using global::Avalonia.Data.Converters;
using global::Avalonia.Media;

public abstract class NullToObjectConverter<T> : IValueConverter
{
    public T NullValue { get; set; } = default!;

    public T NonNullValue { get; set; } = default!;

    public bool HandleEmptyString { get; set; }

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if ((value is null) ||
            (HandleEmptyString && value is string { Length: 0 }))
        {
            return NullValue;
        }

        return NonNullValue;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return AvaloniaProperty.UnsetValue;
    }
}

public sealed class NullToBoolConverter : NullToObjectConverter<bool>
{
    public NullToBoolConverter()
    {
        NullValue = false;
        NonNullValue = true;
    }
}

public sealed class NullToTextConverter : NullToObjectConverter<string?>
{
}

public sealed class NullToBrushConverter : NullToObjectConverter<IBrush>
{
    public NullToBrushConverter()
    {
        NullValue = Brushes.Transparent;
        NonNullValue = Brushes.Transparent;
    }
}

public sealed class NullToColorConverter : NullToObjectConverter<Color>
{
    public NullToColorConverter()
    {
        NullValue = Colors.Transparent;
        NonNullValue = Colors.Transparent;
    }
}
