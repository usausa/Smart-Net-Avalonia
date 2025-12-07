namespace Smart.Avalonia.Data;

using System.Globalization;
using System.Runtime.CompilerServices;

using global::Avalonia.Data.Converters;

public sealed class AnyConverter : IMultiValueConverter
{
    public bool Invert { get; set; }

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        foreach (var value in values)
        {
            if (ConvertToBoolean(value, culture))
            {
                return !Invert;
            }
        }

        return Invert;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool ConvertToBoolean(object? value, CultureInfo culture) =>
        value is not null && System.Convert.ToBoolean(value, culture);
}
