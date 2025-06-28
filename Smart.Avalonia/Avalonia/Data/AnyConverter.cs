namespace Smart.Avalonia.Data;

using System.Globalization;

using global::Avalonia.Data.Converters;

using Smart.Linq;

public sealed class AnyConverter : IMultiValueConverter
{
    public bool Invert { get; set; }

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        foreach (var value in values)
        {
            if (System.Convert.ToBoolean(value, culture))
            {
                return !Invert;
            }
        }

        return Invert;
    }
}
