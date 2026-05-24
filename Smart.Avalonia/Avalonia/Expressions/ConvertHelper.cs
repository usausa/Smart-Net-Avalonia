namespace Smart.Avalonia.Expressions;

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

public static class ConvertHelper
{
    [UnconditionalSuppressMessage("ReflectionAnalysis", "IL2067", Justification = "TypeDescriptor.GetConverter requires type metadata that callers should preserve via DynamicDependency or DynamicallyAccessedMembers.")]
    [UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026", Justification = "TypeDescriptor.GetConverter uses reflection internally. Callers must ensure target types are preserved.")]
    public static object? Convert(Type targetType, object value)
    {
        if (targetType == value.GetType())
        {
            return value;
        }

        if (value is string str)
        {
            var typeConverter = TypeDescriptor.GetConverter(targetType);
            if (typeConverter.CanConvertFrom(typeof(string)))
            {
                return typeConverter.ConvertFromInvariantString(str);
            }
        }

#pragma warning disable CA1031
        try
        {
            return System.Convert.ChangeType(value, targetType, CultureInfo.CurrentCulture);
        }
        catch (Exception)
        {
            return null;
        }
    }
#pragma warning restore CA1031
}
