namespace Smart.Avalonia.Data;

using System.ComponentModel;
using System.Globalization;
using System.Reflection;

using global::Avalonia;
using global::Avalonia.Data.Converters;

public sealed class EnumDescriptionConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return AvaloniaProperty.UnsetValue;
        }

        if (value is Enum)
        {
            var type = value.GetType();
            var mis = type.GetMember(value.ToString()!);
            if (mis.Length > 0)
            {
                var attr = mis[0].GetCustomAttribute<DescriptionAttribute>();
                if (attr is not null)
                {
                    return attr.Description;
                }
            }
        }

        return value.ToString();
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return AvaloniaProperty.UnsetValue;
    }
}
