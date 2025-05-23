namespace Smart.Avalonia.Data;

using System.Collections;
using System.Globalization;

using global::Avalonia;
using global::Avalonia.Data.Converters;
using global::Avalonia.Media;

public abstract class ContainsConverter<T> : IValueConverter
{
    public T TrueValue { get; set; } = default!;

    public T FalseValue { get; set; } = default!;

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return parameter is IList list && list.Contains(value) ? TrueValue : FalseValue;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return AvaloniaProperty.UnsetValue;
    }
}

public sealed class ContainsToBoolConverter : ContainsConverter<bool>
{
    public ContainsToBoolConverter()
    {
        TrueValue = true;
        FalseValue = false;
    }
}

public sealed class ContainsToTextConverter : ContainsConverter<string?>
{
}

public sealed class ContainsToBrushConverter : ContainsConverter<IBrush>
{
    public ContainsToBrushConverter()
    {
        TrueValue = Brushes.Transparent;
        FalseValue = Brushes.Transparent;
    }
}

public sealed class ContainsToColorConverter : ContainsConverter<Color>
{
    public ContainsToColorConverter()
    {
        TrueValue = Colors.Transparent;
        FalseValue = Colors.Transparent;
    }
}
