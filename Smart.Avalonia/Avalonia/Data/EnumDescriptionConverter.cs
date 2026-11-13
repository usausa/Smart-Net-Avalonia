namespace Smart.Avalonia.Data;

using System.Globalization;

using global::Avalonia;
using global::Avalonia.Data;
using global::Avalonia.Data.Converters;

public sealed class EnumDescriptionConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return AvaloniaProperty.UnsetValue;
        }

        if (value is Enum e)
        {
            return Enums.GetDescription(e);
        }

        return value.ToString();
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return BindingOperations.DoNothing;
    }
}
