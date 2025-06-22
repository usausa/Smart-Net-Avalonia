namespace Smart.Avalonia.Data;

using System.Globalization;

using global::Avalonia.Data.Converters;

using Smart.Linq;

public sealed class AllConverter : IMultiValueConverter
{
    public bool Invert { get; set; }

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        return values.All(culture, static (x, s) => System.Convert.ToBoolean(x, s)) ? !Invert : Invert;
    }
}
