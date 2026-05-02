namespace Smart.Avalonia.Data;

using System.Globalization;
using System.Runtime.CompilerServices;

using global::Avalonia.Data.Converters;

public sealed class AllConverter : IMultiValueConverter
{
    private static readonly object BoxedTrue = true;
    private static readonly object BoxedFalse = false;

    public bool Invert { get; set; }

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        foreach (var value in values)
        {
            if (!ConvertToBoolean(value, culture))
            {
                return Invert ? BoxedTrue : BoxedFalse;
            }
        }

        return Invert ? BoxedFalse : BoxedTrue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool ConvertToBoolean(object? value, CultureInfo culture) =>
        value is bool boolValue ? boolValue : value is not null && System.Convert.ToBoolean(value, culture);
}
