namespace Smart.Avalonia.Interactivity;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;

using global::Avalonia;
using global::Avalonia.Xaml.Interactivity;

using Smart.Linq;

public sealed class ResolveMethodAction : StyledElementAction
{
    public static readonly StyledProperty<object?> TargetObjectProperty =
        AvaloniaProperty.Register<ResolveMethodAction, object?>(nameof(TargetObject));

    public static readonly StyledProperty<string> MethodNameProperty =
        AvaloniaProperty.Register<ResolveMethodAction, string>(nameof(MethodName), string.Empty);

    public object? TargetObject
    {
        get => GetValue(TargetObjectProperty);
        set => SetValue(TargetObjectProperty, value);
    }

    public string MethodName
    {
        get => GetValue(MethodNameProperty);
        set => SetValue(MethodNameProperty, value);
    }

    private MethodInfo? cachedMethod;

    private Type? cachedType;

    [UnconditionalSuppressMessage("Trimming", "IL2072", Justification = "Target type is determined at runtime via XAML; callers must ensure the type is preserved")]
    [UnconditionalSuppressMessage("AOT", "IL3050", Justification = "MethodInfo.Invoke is used at runtime; not AOT-safe by design")]
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

        var methodName = MethodName;
        if (String.IsNullOrEmpty(methodName))
        {
            return false;
        }

        if ((cachedMethod is null) ||
            (cachedType != target.GetType()) ||
            (cachedMethod.Name != methodName))
        {
            cachedMethod = target.GetType().GetRuntimeMethods().FirstOrDefault(methodName, static (m, s) =>
                m.Name == s &&
                (m.GetParameters().Length == 0));
            if (cachedMethod is null)
            {
                return false;
            }

            cachedType = target.GetType();
        }

        args.Result = cachedMethod.Invoke(target, null);
        return true;
    }
}
