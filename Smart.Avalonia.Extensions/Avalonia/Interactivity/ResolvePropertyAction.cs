namespace Smart.Avalonia.Interactivity;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;

using global::Avalonia;
using global::Avalonia.Xaml.Interactivity;

public sealed class ResolvePropertyAction : StyledElementAction
{
    public static readonly StyledProperty<object?> TargetObjectProperty =
        AvaloniaProperty.Register<ResolvePropertyAction, object?>(nameof(TargetObject));

    public static readonly StyledProperty<string> PropertyNameProperty =
        AvaloniaProperty.Register<ResolvePropertyAction, string>(nameof(PropertyName), string.Empty);

    public object? TargetObject
    {
        get => GetValue(TargetObjectProperty);
        set => SetValue(TargetObjectProperty, value);
    }

    public string PropertyName
    {
        get => GetValue(PropertyNameProperty);
        set => SetValue(PropertyNameProperty, value);
    }

    private PropertyInfo? cachedProperty;

    [UnconditionalSuppressMessage("Trimming", "IL2072", Justification = "Target type is determined at runtime via XAML; callers must ensure the type is preserved")]
    [UnconditionalSuppressMessage("AOT", "IL3050", Justification = "PropertyInfo.GetValue is used at runtime; not AOT-safe by design")]
    public override object Execute(object? sender, object? parameter)
    {
        if (!IsEnabled)
        {
            return false;
        }

        if (parameter is not Smart.Mvvm.Messaging.ResolveEventArgs args)
        {
            return false;
        }

        var target = TargetObject ?? sender;
        if (target is null)
        {
            return false;
        }

        var propertyName = PropertyName;
        if (String.IsNullOrEmpty(propertyName))
        {
            return false;
        }

        if ((cachedProperty is null) ||
            (cachedProperty.DeclaringType != target.GetType()) ||
            (cachedProperty.Name != propertyName))
        {
            cachedProperty = target.GetType().GetRuntimeProperty(propertyName);
            if (cachedProperty is null)
            {
                return false;
            }
        }

        args.Result = cachedProperty.GetValue(target);
        return true;
    }
}
